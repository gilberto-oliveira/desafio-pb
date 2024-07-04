using MediatR;

namespace DesafioPB.CadastroCliente.Commands;

public record NovoClienteCommand(string nome, string? sobrenome, string cpf, string email) : IRequest<(bool, int?)>
{
  public static implicit operator Contexts.Entities.Cliente(NovoClienteCommand cliente)
    => new Contexts.Entities.Cliente
    {
      Nome = cliente.nome,
      Sobrenome = cliente.sobrenome,
      Cpf = cliente.cpf,
      Email = cliente.email
    };
}
