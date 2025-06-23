using AutoMapper;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using CadastroDeClienteMuralis.Aplicacao.Interfaces;
using CadastroDeClienteMuralis.Dominio.Entidades;
using CadastroDeClienteMuralis.Dominio.Exceptions;
using CadastroDeClienteMuralis.Dominio.Repositorios;
using Microsoft.Extensions.Logging;

namespace CadastroDeClienteMuralis.Aplicacao.Servicos;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly IViaCepApiService _viaCepApiService;
    private readonly ILogger<ClienteService> _logger;


    public ClienteService(
        IClienteRepository clienteRepository,
        IMapper mapper,
        IViaCepApiService viaCepApiService,
        ILogger<ClienteService> logger)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _viaCepApiService = viaCepApiService;
        _logger = logger;
    }

    public async Task<ClienteResponse> CriarAsync(CreateClienteRequest request)
    {
        _logger.LogInformation("Iniciando criação de cliente.");

        await ValidarDuplicidadeClienteAsync(request);

        var cliente = _mapper.Map<Cliente>(request);

        if (request.Endereco != null && !string.IsNullOrWhiteSpace(request.Endereco.Cep))
        {
            try
            {
                _logger.LogInformation("Consultando endereço no ViaCEP para o CEP: {Cep}", request.Endereco.Cep);

                var enderecoViaCep = await _viaCepApiService.ObterEnderecoPorCepAsync(request.Endereco.Cep);

                if (!enderecoViaCep.Sucesso)
                {
                    _logger.LogWarning("ViaCEP retornou erro: {Erro}", enderecoViaCep.Erro);
                    throw new ClienteException(enderecoViaCep.Erro);
                }

                cliente.AtualizarEndereco(new Endereco(
                    cep: request.Endereco.Cep,
                    logradouro: enderecoViaCep.Logradouro,
                    cidade: enderecoViaCep.Localidade,
                    numero: request.Endereco.Numero,
                    complemento: request.Endereco.Complemento
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar o CEP {Cep}", request.Endereco.Cep);
                throw new ClienteException($"Erro ao consultar o CEP: {ex.Message}");
            }
        }

        await _clienteRepository.AdicionarAsync(cliente);

        _logger.LogInformation("Cliente criado com sucesso. ID: {Id}", cliente.Id);

        return _mapper.Map<ClienteResponse>(cliente);
    }

    public async Task AtualizarAsync(UpdateClienteRequest request)
    {
        _logger.LogInformation("Iniciando atualização do cliente {Id}", request.Id);

        var cliente = await _clienteRepository.ObterClientePorIdAsync(request.Id)
            ?? throw new ClienteException("Cliente não encontrado.");

        cliente.AtualizarNome(request.Nome);

        if (request.Endereco != null)
            cliente.AtualizarEndereco(await ObterEnderecoPreenchidoAsync(request.Endereco));

        var contatosAtualizados = request.Contatos?
            .Select(c => new Contato(c.Id, c.Tipo, c.Texto))
            .ToList() ?? new List<Contato>();

        ValidarContatos(contatosAtualizados, cliente);

        cliente.AtualizarContatos(contatosAtualizados);

        await _clienteRepository.AtualizarAsync(cliente);

        _logger.LogInformation("Cliente {Id} atualizado com sucesso.", request.Id);
    }

    public async Task DeletarAsync(Guid id)
    {
        _logger.LogInformation("Iniciando deleção do cliente {Id}", id);

        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        if (cliente is null)
            throw new ClienteException("Cliente não encontrado.");

        await _clienteRepository.RemoverAsync(cliente);

        _logger.LogInformation("Cliente {Id} deletado com sucesso.", id);
    }

    public async Task<ClienteResponse> ObterPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando cliente por ID: {Id}", id);

        var cliente = await _clienteRepository.ObterClientePorIdAsync(id);

        if (cliente == null)
        {
            _logger.LogWarning("Cliente {Id} não encontrado.", id);
            return null;
        }

        return _mapper.Map<ClienteResponse>(cliente);
    }

    public async Task<List<ClienteResponse>> ObterTodosAsync()
    {
        _logger.LogInformation("Buscando todos os clientes.");

        var clientes = await _clienteRepository.ObterTodosClientesAsync();

        return _mapper.Map<List<ClienteResponse>>(clientes);
    }

    private async Task<Endereco> ObterEnderecoPreenchidoAsync(UpdateEnderecoRequest enderecoRequest)
    {
        _logger.LogInformation("Consultando ViaCEP para o CEP: {Cep}", enderecoRequest.Cep);

        var enderecoViaCep = await _viaCepApiService.ObterEnderecoPorCepAsync(enderecoRequest.Cep);

        if (!enderecoViaCep.Sucesso)
        {
            _logger.LogWarning("ViaCEP retornou erro: {Erro}", enderecoViaCep.Erro);
            throw new ClienteException(enderecoViaCep.Erro);
        }

        return new Endereco(
            cep: enderecoRequest.Cep,
            logradouro: enderecoViaCep.Logradouro,
            cidade: enderecoViaCep.Localidade,
            numero: enderecoRequest.Numero,
            complemento: enderecoRequest.Complemento
        );
    }

    private void ValidarContatos(List<Contato> contatosAtualizados, Cliente cliente)
    {
        var contatosInvalidos = contatosAtualizados
            .Where(c => c.Id != Guid.Empty && !cliente.Contatos.Any(existing => existing.Id == c.Id))
            .ToList();

        if (contatosInvalidos.Any())
        {
            var ids = string.Join(", ", contatosInvalidos.Select(c => c.Id));
            _logger.LogWarning("IDs de contato inválidos encontrados: {Ids}", ids);
            throw new ClienteException($"Os seguintes IDs de contato não pertencem ao cliente: {ids}");
        }
    }
    private async Task ValidarDuplicidadeClienteAsync(CreateClienteRequest request)
    {
        if (request.Endereco == null || string.IsNullOrWhiteSpace(request.Endereco.Cep))
            return;

        var clienteExistente = await _clienteRepository.ObterPorNomeECepAsync(request.Nome, request.Endereco.Cep);

        if (clienteExistente != null)
        {
            _logger.LogWarning(
                "Tentativa de criar cliente duplicado. Nome: {Nome}, CEP: {Cep}",
                request.Nome,
                request.Endereco.Cep);

            throw new ClienteException(
                $"Já existe um cliente com o nome '{request.Nome}' no endereço informado.");
        }
    }
}
