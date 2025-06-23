using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using CadastroDeClienteMuralis.Aplicacao.Interfaces;
using CadastroDeClienteMuralis.Dominio.Mediador.Queries;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Handlers
{
    public class ClienteQueryHandler :
        IRequestHandler<ObterClientePorIdQuery, ClienteResponse>,
        IRequestHandler<ListarClientesQuery, List<ClienteResponse>>
    {
        private readonly IClienteService _clienteService;

        public ClienteQueryHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<ClienteResponse> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteService.ObterPorIdAsync(request.Id);

            if (cliente is null)
                return null;

            return cliente;
        }

        public async Task<List<ClienteResponse>> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteService.ObterTodosAsync();
            return clientes;
        }
    }
}
