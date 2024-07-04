using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CadastroCliente.Contexts;

[ExcludeFromCodeCoverage]
public class CadastroClienteDbContext : DbContext
{
  public CadastroClienteDbContext(DbContextOptions<CadastroClienteDbContext> options) : base(options)
  { }

  public DbSet<Entities.Cliente> Clientes { get; set; }
  public DbSet<Entities.NovoCartaoDeCreditoInfo> NovoCartaoDeCreditoInfos { get; set; }
  public DbSet<Entities.NovaPropostaCreditoInfo> NovaPropostaCreditoInfos { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}
