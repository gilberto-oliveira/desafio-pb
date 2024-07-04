
using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;

namespace DesafioPB.CadastroCliente.Messaging;

public static class MessageBusExtensions
{
  public static void AddMessaging(this IServiceCollection services)
  {
    services.AddMessagingBus();
    services.AddScoped<INotificationContext, NotificationContext>();
  }

  public static void AddMessagingConsumer(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMessagingBus();
    services.AddHostedService<NovoCartaoDeCreditoService>();
    services.AddHostedService<NovaPropostaCreditoService>();
  }
}