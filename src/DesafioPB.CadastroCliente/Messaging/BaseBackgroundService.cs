using DesafioPB.Common.Messaging;

namespace DesafioPB.CadastroCliente.Messaging;

public abstract class BaseBackgroundService<TEvent> : BackgroundService where TEvent : class
{
  private readonly IMessageBus bus;
  protected readonly IServiceProvider serviceProvider;

  protected BaseBackgroundService(IMessageBus bus, IServiceProvider serviceProdiver)
  {
    this.bus = bus;
    this.serviceProvider = serviceProdiver;
  }
  protected abstract Task ProcessEventAsync(TEvent arg);

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await bus.SubscribeAsync<TEvent>(string.Empty, ProcessEventAsync, stoppingToken);
  }
}