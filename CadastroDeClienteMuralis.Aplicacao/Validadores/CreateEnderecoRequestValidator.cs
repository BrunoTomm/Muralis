using CadastroDeClienteMuralis.Aplicacao.DTO.Request;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using FluentValidation;

namespace CadastroDeClienteMuralis.Aplicacao.Validators;

public class CreateEnderecoRequestValidator : AbstractValidator<CreateEnderecoRequest>
{
    public CreateEnderecoRequestValidator()
    {
        RuleFor(e => e)
            .Must(e => e == null ||
                       (!string.IsNullOrWhiteSpace(e.Cep) &&
                        !string.IsNullOrWhiteSpace(e.Numero)))
            .WithMessage("Ou informe todos os campos obrigatórios do endereço ou envie nulo.");

        When(e => e != null, () =>
        {
            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O campo CEP é obrigatório.")
                .Length(8).WithMessage("O CEP deve conter 8 caracteres.");
        });
    }
}
