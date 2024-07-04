using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CadastroCliente.Contexts.Entities;
using DesafioPB.Common.Messaging;

namespace DesafioPB.CadastroCliente.Messaging;

public class NovaPropostaCreditoService : BaseBackgroundService<PropostaCreditoInfoEvent>
{
  public NovaPropostaCreditoService(IMessageBus bus, IServiceProvider sp) : base(bus, sp) { }

  protected override async Task ProcessEventAsync(PropostaCreditoInfoEvent item)
  {
    if (item is null) return;

    using var serviceScope = serviceProvider.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<CadastroClienteDbContext>()!;

    NovaPropostaCreditoInfo info = new()
    {
      ClienteId = item.clienteId,
      ClienteCpf = item.clienteCpf,
      Limite = item.limite,
      Mensagem = item.mensagem
    };
    
    await context.NovaPropostaCreditoInfos.AddAsync(info);

    await context.SaveChangesAsync();
  }
}
