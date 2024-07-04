using MediatR;

namespace DesafioPB.CadastroCliente.Clientes.Queries;

public record ListarClientesQuery : IRequest<IEnumerable<ListarClientesViewModel>>
{
  public ListarClientesQuery(string nome, string? sobrenome, string email, string cpf)
  {
    Nome = nome;
    Sobrenome = sobrenome;
    Email = email;
    Cpf = cpf;
  }

  public const string CacheKey = nameof(ListarClientesQuery);
  public string Nome { get; }
  public string? Sobrenome { get; }
  public string Email { get; }
  public string Cpf { get; } 
}
