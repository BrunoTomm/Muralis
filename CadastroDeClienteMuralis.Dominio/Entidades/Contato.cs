namespace CadastroDeClienteMuralis.Dominio.Entidades
{
    public class Contato
    {
        public Guid Id { get; private set; }
        public string Tipo { get; private set; }
        public string Texto { get; private set; }

        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }

        protected Contato() { }

        public Contato(string tipo, string texto)
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Texto = texto;
        }

        public Contato(Guid id, string tipo, string texto)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Tipo = tipo;
            Texto = texto;
        }

        public void Atualizar(string tipo, string texto)
        {
            Tipo = tipo;
            Texto = texto;
        }

        public void DefinirCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
