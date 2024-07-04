using DesafioPB.Common.Messaging;
using FluentValidation;

namespace DesafioPB.PropostaCredito.Services.Validators;

public class NovaPropostaCreditoValidator : AbstractValidator<CadastroClienteAddedEvent>
{
  public NovaPropostaCreditoValidator()
  {
    ClassLevelCascadeMode = CascadeMode.Continue;

    RuleFor(b => b.cpf)
        .NotEmpty()
        .NotNull();

        RuleFor(b => b.id)
            .NotEqual(0);

    }
}