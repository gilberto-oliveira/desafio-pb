using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CadastroCliente.Contexts.Entities;
using DesafioPB.Common.Messaging;

namespace DesafioPB.CadastroCliente.Messaging;

public class NovoCartaoDeCreditoService : BaseBackgroundService<CartaoDeCreditoInfoEvent>
{
  public NovoCartaoDeCreditoService(IMessageBus bus, IServiceProvider sp) : base(bus, sp) { }

  protected override async Task ProcessEventAsync(CartaoDeCreditoInfoEvent item)
  {
    if (item is null) return;

    using var serviceScope = serviceProvider.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<CadastroClienteDbContext>()!;

    NovoCartaoDeCreditoInfo info = new()
    {
      ClienteId = item.clienteId,
      Numero = item.numero,
      Cvv = item.cvv,
      Validade = item.validade,
      Mensagem = item.mensagem
    };
    
    await context.NovoCartaoDeCreditoInfos.AddAsync(info);

    await context.SaveChangesAsync();
  }
}
