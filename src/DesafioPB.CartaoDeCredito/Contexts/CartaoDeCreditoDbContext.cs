using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CartaoDeCredito.Contexts;

[ExcludeFromCodeCoverage]
public class CartaoDeCreditoDbContext : DbContext
{
  public CartaoDeCreditoDbContext(DbContextOptions<CartaoDeCreditoDbContext> options) : base(options)
  { }

  public DbSet<Entities.CartaoDeCredito> CartaoDeCreditos { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}
