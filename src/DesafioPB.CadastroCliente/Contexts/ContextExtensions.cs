using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CadastroCliente.Contexts;

[ExcludeFromCodeCoverage]
public static class ContextExtensions
{
  public static void AddContextServices(this IServiceCollection services)
  {
    services.AddDbContext<CadastroClienteDbContext>(options
      => options.UseInMemoryDatabase("cadastro_cliente"));
  }
}