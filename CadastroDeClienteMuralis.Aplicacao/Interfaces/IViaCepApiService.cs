using CadastroDeClienteMuralis.Aplicacao.DTO.Response;

namespace CadastroDeClienteMuralis.Aplicacao.Interfaces
{
    public interface IViaCepApiService
    {
        Task<EnderecoResponseViaCep> ObterEnderecoPorCepAsync(string cep);
    }
}
