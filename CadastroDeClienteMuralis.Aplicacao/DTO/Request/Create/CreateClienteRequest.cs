namespace CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;

public class CreateClienteRequest
{
    public string Nome { get; set; } = string.Empty;
    public CreateEnderecoRequest Endereco { get; set; }
    public List<CreateContatoRequest> Contatos { get; set; }
}
