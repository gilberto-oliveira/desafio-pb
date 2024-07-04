using System.Diagnostics.CodeAnalysis;

namespace DesafioPB.PropostaCredito.Contexts.Entities;

[ExcludeFromCodeCoverage]
public class PropostaCredito
{
    public int Id { get; set; }
    public required int ClienteId { get; set; }
    public required decimal Limite { get; set; }
    public required string ClienteCpf { get; set; }
}