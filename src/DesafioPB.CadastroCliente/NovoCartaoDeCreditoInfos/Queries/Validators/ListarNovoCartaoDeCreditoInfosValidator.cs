using FluentValidation;

namespace DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries.Validations;

public class ListarNovoCartaoDeCreditoInfosValidator : AbstractValidator<ListarNovoCartaoDeCreditoInfosQuery>
{
  public ListarNovoCartaoDeCreditoInfosValidator()
  {
    ClassLevelCascadeMode = CascadeMode.Continue;

    RuleFor(b => b.ClienteId)
        .NotEqual(0);
    }
}