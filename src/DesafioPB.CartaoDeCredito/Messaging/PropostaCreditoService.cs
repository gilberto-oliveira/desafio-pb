using DesafioPB.CartaoDeCredito.Services;
using DesafioPB.Common.Messaging;

namespace DesafioPB.CartaoDeCredito.Messaging;

public class PropostaCreditoService : BaseBackgroundService<PropostaCreditoAddedEvent>
{
  public PropostaCreditoService(IMessageBus bus, IServiceProvider sp) : base(bus, sp) { }

  protected override async Task ProcessEventAsync(PropostaCreditoAddedEvent item)
  {
    if (item is null) return;
    using var serviceScope = serviceProvider.CreateScope();
    var _novaPropostaClienteService = serviceScope.ServiceProvider.GetService<IService>()!;
    await _novaPropostaClienteService.Handle(item, new CancellationToken());
  }
}
