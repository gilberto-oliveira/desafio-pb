using DesafioPB.Common.Messaging;
using FluentValidation;

namespace DesafioPB.CartaoDeCredito.Services.Validators;

public class NovoCartaoDeCreditoValidator : AbstractValidator<PropostaCreditoAddedEvent>
{
  public NovoCartaoDeCreditoValidator()
  {
    ClassLevelCascadeMode = CascadeMode.Continue;

    RuleFor(b => b.limite)
        .NotEqual(0);

    RuleFor(b => b.clienteCpf)
    .NotEmpty()
    .NotNull();

    RuleFor(b => b.clienteId)
        .NotEqual(0);
    }
}