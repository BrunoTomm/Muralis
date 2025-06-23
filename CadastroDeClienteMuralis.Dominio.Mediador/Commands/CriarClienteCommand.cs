using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Commands
{
    public class CriarClienteCommand : IRequest<ClienteResponse>
    {
        public CreateClienteRequest Cliente { get; }

        public CriarClienteCommand(CreateClienteRequest cliente)
        {
            Cliente = cliente;
        }
    }
}
