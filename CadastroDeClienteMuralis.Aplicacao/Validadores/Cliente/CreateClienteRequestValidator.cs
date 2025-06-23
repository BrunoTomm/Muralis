using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.Validators;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validadores.Cliente
{
    public class CreateClienteRequestValidator : AbstractValidator<CreateClienteRequest>
    {
        public CreateClienteRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(c => c.Endereco)
                .SetValidator(new CreateEnderecoRequestValidator())
                .When(c => c.Endereco != null);

            RuleForEach(c => c.Contatos)
                .SetValidator(new CreateContatoRequestValidator());
        }
    }
}
