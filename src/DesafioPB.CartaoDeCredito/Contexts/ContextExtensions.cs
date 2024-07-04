using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CartaoDeCredito.Contexts;

[ExcludeFromCodeCoverage]
public static class ContextExtensions
{
  public static void AddContextServices(this IServiceCollection services)
  {
    services.AddDbContext<CartaoDeCreditoDbContext>(options
      => options.UseInMemoryDatabase("cartao_de_credito"));
  }
}