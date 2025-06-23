using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validators
{
    public class UpdateEnderecoRequestValidator : AbstractValidator<UpdateEnderecoRequest>
    {
        public UpdateEnderecoRequestValidator()
        {
        }
    }
}
