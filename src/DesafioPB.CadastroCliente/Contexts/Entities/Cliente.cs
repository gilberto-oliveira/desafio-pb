using System.Diagnostics.CodeAnalysis;

namespace DesafioPB.CadastroCliente.Contexts.Entities;

[ExcludeFromCodeCoverage]
public class Cliente
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Sobrenome { get; set; }
    public required string Cpf { get; set; }
    public required string Email { get; set; }
}