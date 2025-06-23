using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Commands;

public class AtualizarClienteCommand : IRequest<Unit>
{
    public UpdateClienteRequest Cliente { get; }

    public AtualizarClienteCommand(UpdateClienteRequest cliente)
    {
        Cliente = cliente;
    }
}
