using DesafioPB.Common.Messaging;
using DesafioPB.PropostaCredito.Services;

namespace DesafioPB.PropostaCredito.Messaging;

public class CadastroClienteService : BaseBackgroundService<CadastroClienteAddedEvent>
{
  public CadastroClienteService(IMessageBus bus, IServiceProvider sp) : base(bus, sp) { }

  protected override async Task ProcessEventAsync(CadastroClienteAddedEvent item)
  {
    if (item is null) return;
    using var serviceScope = serviceProvider.CreateScope();
    var _novaPropostaClienteService = serviceScope.ServiceProvider.GetService<IService>()!;
    await _novaPropostaClienteService.Handle(item, new CancellationToken());
  }
}
