using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Queries
{
    public class ObterClientePorIdQuery : IRequest<ClienteResponse>
    {
        public Guid Id { get; }

        public ObterClientePorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
