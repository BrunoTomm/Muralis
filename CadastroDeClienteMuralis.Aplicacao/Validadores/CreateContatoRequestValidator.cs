using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validators
{
    public class CreateContatoRequestValidator : AbstractValidator<CreateContatoRequest>
    {
        public CreateContatoRequestValidator()
        {
        }
    }
}
