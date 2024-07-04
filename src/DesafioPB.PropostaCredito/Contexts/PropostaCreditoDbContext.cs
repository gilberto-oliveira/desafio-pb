using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.PropostaCredito.Contexts;

[ExcludeFromCodeCoverage]
public class PropostaCreditoDbContext : DbContext
{
  public PropostaCreditoDbContext(DbContextOptions<PropostaCreditoDbContext> options) : base(options)
  { }

  public DbSet<Entities.PropostaCredito> PropostaCreditos { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}
