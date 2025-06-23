using CadastroDeClienteMuralis.Dominio.Entidades;
using CadastroDeClienteMuralis.Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClienteMuralis.Infra.Contextos
{
    public class ContextoCadastroDeCliente : DbContext
    {
        public ContextoCadastroDeCliente(DbContextOptions<ContextoCadastroDeCliente> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfig());
            modelBuilder.ApplyConfiguration(new EnderecoConfig());
            modelBuilder.ApplyConfiguration(new ContatoConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
