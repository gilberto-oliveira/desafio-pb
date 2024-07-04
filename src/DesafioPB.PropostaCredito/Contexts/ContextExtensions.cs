using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.PropostaCredito.Contexts;

[ExcludeFromCodeCoverage]
public static class ContextExtensions
{
  public static void AddContextServices(this IServiceCollection services)
  {
    services.AddDbContext<PropostaCreditoDbContext>(options
      => options.UseInMemoryDatabase("proposta_credito"));
  }
}