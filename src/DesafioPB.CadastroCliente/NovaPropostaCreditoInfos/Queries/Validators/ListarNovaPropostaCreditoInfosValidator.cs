using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries;
using FluentValidation;

namespace DesafioPB.CadastroCliente.NovaPropostaCreditoInfos.Queries.Validations;

public class ListarNovaPropostaCreditoInfosValidator : AbstractValidator<ListarNovaPropostaCreditoInfosQuery>
{
  public ListarNovaPropostaCreditoInfosValidator()
  {
    ClassLevelCascadeMode = CascadeMode.Continue;

    RuleFor(b => b.ClienteId)
        .NotEqual(0);
 }
}