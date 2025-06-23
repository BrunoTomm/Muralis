using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using MediatR;

namespace CadastroDeClienteMuralis.Dominio.Mediador.Queries
{
    public class ListarClientesQuery : IRequest<List<ClienteResponse>>
    {
        public ListarClientesQuery() { }
    }
}
