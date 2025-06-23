using CadastroDeClienteMuralis.Dominio.Repositorios;
using CadastroDeClienteMuralis.Infra.Contextos;
using CadastroDeClienteMuralis.Infra.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClienteMuralis.Infra.Repositorios
{
    public class ClienteRepository : RepositorioBase<Cliente>, IClienteRepository
    {
        private readonly ContextoCadastroDeCliente _contexto;

        public ClienteRepository(ContextoCadastroDeCliente contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cliente> ObterPorIdComEnderecoEContatosAsync(Guid id)
        {
            return await _contexto.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cliente>> ObterTodosClientesAsync()
        {
            return await _contexto.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .ToListAsync();
        }

        public async Task<Cliente> ObterClientePorIdAsync(Guid id)
        {
            return await _contexto.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterPorNomeECepAsync(string nome, string cep)
        {
            return await _contexto.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c =>
                    c.Nome.ToLower() == nome.ToLower() &&
                    c.Endereco != null &&
                    c.Endereco.Cep == cep);
        }
    }
}
