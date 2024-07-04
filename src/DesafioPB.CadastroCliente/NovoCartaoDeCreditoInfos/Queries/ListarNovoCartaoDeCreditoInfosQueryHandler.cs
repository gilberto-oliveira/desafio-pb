using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos;
using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioPB.CadastroCliente.Clientes.Queries;

public class ListarNovoCartaoDeCreditoInfosQueryHandler :
 IRequestHandler<ListarNovoCartaoDeCreditoInfosQuery, IEnumerable<ListarNovoCartaoDeCreditoInfosViewModel>>
{
    private readonly CadastroClienteDbContext context;

    public ListarNovoCartaoDeCreditoInfosQueryHandler(CadastroClienteDbContext context) => this.context = context;

    public async Task<IEnumerable<ListarNovoCartaoDeCreditoInfosViewModel>> Handle(ListarNovoCartaoDeCreditoInfosQuery request, CancellationToken cancellationToken)
    {
        return await context.NovoCartaoDeCreditoInfos
                .WhereBy(request)
                .Select(s => new ListarNovoCartaoDeCreditoInfosViewModel(s.Id, s.ClienteId, s.Numero, s.Cvv, s.Validade, s.Mensagem))
                .ToListAsync(cancellationToken);
    }
}

public static class ListarNovoCartaoDeCreditoInfosQueryHandlerExtensions
{
    public static IQueryable<Contexts.Entities.NovoCartaoDeCreditoInfo> WhereBy(this IQueryable<Contexts.Entities.NovoCartaoDeCreditoInfo> info, ListarNovoCartaoDeCreditoInfosQuery query)
      => info.Where(b => b.ClienteId == query.ClienteId  && 
      (query.Numero == null || b.Numero == query.Numero));
}