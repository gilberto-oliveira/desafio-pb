using DesafioPB.CadastroCliente.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CadastroCliente.Clientes.Queries;

public class ListarClientesQueryHandler :
 IRequestHandler<ListarClientesQuery, IEnumerable<ListarClientesViewModel>>
{
    private readonly CadastroClienteDbContext context;

    public ListarClientesQueryHandler(CadastroClienteDbContext context) => this.context = context;

    public async Task<IEnumerable<ListarClientesViewModel>> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
    {
        return await context.Clientes
                .WhereBy(request)
                .Select(s => new ListarClientesViewModel(s.Nome, s.Sobrenome, s.Email, s.Cpf))
                .ToListAsync(cancellationToken);
    }
}

public static class ClientesDisponiveisHandlerExtensions
{
    public static IQueryable<Contexts.Entities.Cliente> WhereBy(this IQueryable<Contexts.Entities.Cliente> cliente, ListarClientesQuery query)
      => cliente.Where(b => b.Nome == query.Nome  && 
        b.Email == query.Email && 
      (query.Sobrenome == null || b.Sobrenome == query.Sobrenome) && b.Cpf == query.Cpf);
}