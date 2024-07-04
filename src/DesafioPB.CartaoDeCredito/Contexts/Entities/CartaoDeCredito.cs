using System.Diagnostics.CodeAnalysis;

namespace DesafioPB.CartaoDeCredito.Contexts.Entities;

[ExcludeFromCodeCoverage]
public class CartaoDeCredito
{
    public int Id { get; set; }
    public required int ClienteId { get; set; }
    public required string Numero { get; set; }
    public required int Cvv { get; set; }
    public required string Validade { get; set; }
}