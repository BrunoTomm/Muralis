﻿namespace CadastroDeClienteMuralis.Aplicacao.DTO.Response
{
    public class EnderecoResponseViaCep
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; } 
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string Ddd { get; set; }
        public string Siafi { get; set; }

        public bool Sucesso => string.IsNullOrEmpty(Erro);
        public string? Erro { get; set; }
    }
}
