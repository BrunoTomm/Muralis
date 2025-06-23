using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using CadastroDeClienteMuralis.Aplicacao.DTO.Response;

namespace CadastroDeClienteMuralis.Aplicacao.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResponse> CriarAsync(CreateClienteRequest request);
        Task AtualizarAsync(UpdateClienteRequest request);
        Task DeletarAsync(Guid id);
        Task<ClienteResponse> ObterPorIdAsync(Guid id);
        Task<List<ClienteResponse>> ObterTodosAsync();
    }
}
