using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validators
{
    public class UpdateContatoRequestValidator : AbstractValidator<UpdateContatoRequest>
    {
        public UpdateContatoRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O Id do contato é obrigatório para alterações.");
        }
    }
}
