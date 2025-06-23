namespace CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;

public class UpdateClienteRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public UpdateEnderecoRequest Endereco { get; set; }
    public List<UpdateContatoRequest> Contatos { get; set; }
}
