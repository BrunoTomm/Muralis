using CadastroDeClienteMuralis.Dominio.Entidades;

namespace CadastroDeClienteMuralis.Dominio.Repositorios
{
    public interface IClienteRepository : IRepositorioBase<Cliente>
    {
        Task<Cliente> ObterPorIdComEnderecoEContatosAsync(Guid id);
        Task<Cliente> ObterClientePorIdAsync(Guid id);
        Task<Cliente> ObterPorNomeECepAsync(string nome, string cep);
        Task<List<Cliente>> ObterTodosClientesAsync();
    }
}
