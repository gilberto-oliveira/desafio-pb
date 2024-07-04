using FluentValidation;

namespace DesafioPB.CadastroCliente.Commands.Validators;

public class NovoClienteValidator : AbstractValidator<NovoClienteCommand>
{
  public NovoClienteValidator()
  {
    ClassLevelCascadeMode = CascadeMode.Continue;

    RuleFor(b => b.nome)
        .NotEmpty()
        .NotNull();

        RuleFor(b => b.email)
            .NotEmpty()
            .NotNull();

        RuleFor(b => b.cpf)
            .NotEmpty()
            .NotNull();
    }
}