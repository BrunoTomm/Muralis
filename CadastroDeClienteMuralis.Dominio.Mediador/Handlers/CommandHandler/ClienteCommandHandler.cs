using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using CadastroDeClienteMuralis.Aplicacao.Interfaces;
using CadastroDeClienteMuralis.Dominio.Mediador.Commands;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Handlers
{
    public class ClienteCommandHandler :
        IRequestHandler<CriarClienteCommand, ClienteResponse>,
        IRequestHandler<AtualizarClienteCommand, Unit>,
        IRequestHandler<DeletarClienteCommand, Unit>
    {
        private readonly IClienteService _clienteService;

        public ClienteCommandHandler(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<ClienteResponse> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            return await _clienteService.CriarAsync(request.Cliente);
        }

        public async Task<Unit> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteService.AtualizarAsync(request.Cliente);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteService.DeletarAsync(request.Id);
            return Unit.Value;
        }
    }
}
