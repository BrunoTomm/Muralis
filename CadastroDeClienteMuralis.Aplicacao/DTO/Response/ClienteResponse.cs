namespace CadastroDeClienteMuralis.Aplicacao.DTO.Response
{
    public class ClienteResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }

        public EnderecoResponse Endereco { get; set; }
        public List<ContatoResponse> Contatos { get; set; }
    }
}
