using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using CadastroDeClienteMuralis.Aplicacao.Validators;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validadores.Cliente
{
    public class UpdateClienteRequestValidator : AbstractValidator<UpdateClienteRequest>
    {
        public UpdateClienteRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O Id do cliente é obrigatório.");

            RuleFor(c => c.Endereco)
                .SetValidator(new UpdateEnderecoRequestValidator())
                .When(c => c.Endereco != null);

            RuleForEach(c => c.Contatos)
                .SetValidator(new UpdateContatoRequestValidator());
        }
    }
}
