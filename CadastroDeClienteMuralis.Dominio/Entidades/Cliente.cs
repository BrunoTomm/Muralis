using CadastroDeClienteMuralis.Dominio.Entidades;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public Endereco Endereco { get; private set; }
    public ICollection<Contato> Contatos { get; private set; }

    protected Cliente() { }

    public Cliente(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        DataCadastro = DateTime.UtcNow;
        Contatos = new List<Contato>();
    }

    public void AtualizarNome(string nome)
    {
        Nome = nome;
    }

    public void AtualizarContatos(List<Contato> novosContatos)
    {
        Contatos ??= new List<Contato>();

        var paraRemover = Contatos
            .Where(c => !novosContatos.Any(n => n.Id == c.Id))
            .ToList();

        foreach (var contato in paraRemover)
        {
            Contatos.Remove(contato);
        }

        foreach (var novo in novosContatos)
        {
            var existente = Contatos.FirstOrDefault(c => c.Id == novo.Id);

            if (existente != null)
            {
                existente.Atualizar(novo.Tipo, novo.Texto);
            }
            else
            {
                novo.DefinirCliente(Id);
                Contatos.Add(novo);
            }
        }
    }

    public void AtualizarEndereco(Endereco endereco)
    {
        if (Endereco is null)
        {
            Endereco = endereco;
            return;
        }

        Endereco.AtualizarEndereco(
            endereco
        );
    }
}
