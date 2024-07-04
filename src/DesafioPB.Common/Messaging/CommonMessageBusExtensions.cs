using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioPB.Common.Messaging;

public static class CommonMessageBusExtensions
{
  public static void AddMessagingBus(this IServiceCollection services)
  {
    services.AddSingleton<IMessageBus, MessageBus>();
  }
}