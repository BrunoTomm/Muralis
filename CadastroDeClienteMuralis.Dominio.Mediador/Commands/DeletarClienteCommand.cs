using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Commands
{
    public class DeletarClienteCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeletarClienteCommand(Guid id)
        {
            Id = id;
        }
    }
}
