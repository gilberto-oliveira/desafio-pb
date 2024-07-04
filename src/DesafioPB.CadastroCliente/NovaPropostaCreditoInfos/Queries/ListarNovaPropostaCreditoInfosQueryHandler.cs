using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CadastroCliente.NovaPropostaCreditoInfos;
using DesafioPB.CadastroCliente.NovaPropostaCreditoInfos.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CadastroCliente.Clientes.Queries;

public class ListarNovaPropostaCreditoInfosQueryHandler :
 IRequestHandler<ListarNovaPropostaCreditoInfosQuery, IEnumerable<ListarNovaPropostaCreditoInfosViewModel>>
{
    private readonly CadastroClienteDbContext context;

    public ListarNovaPropostaCreditoInfosQueryHandler(CadastroClienteDbContext context) => this.context = context;

    public async Task<IEnumerable<ListarNovaPropostaCreditoInfosViewModel>> Handle(ListarNovaPropostaCreditoInfosQuery request, CancellationToken cancellationToken)
    {
        return await context.NovaPropostaCreditoInfos
                .WhereBy(request)
                .Select(s => new ListarNovaPropostaCreditoInfosViewModel(s.Id, s.ClienteId, s.ClienteCpf, s.Limite, s.Mensagem))
                .ToListAsync(cancellationToken);
    }
}

public static class ListarNovaPropostaCreditoInfosQueryHandlerExtensions
{
    public static IQueryable<Contexts.Entities.NovaPropostaCreditoInfo> WhereBy(this IQueryable<Contexts.Entities.NovaPropostaCreditoInfo> info, ListarNovaPropostaCreditoInfosQuery query)
      => info.Where(b => b.ClienteId == query.ClienteId  && 
      (query.ClienteCpf == null || b.ClienteCpf == query.ClienteCpf));
}