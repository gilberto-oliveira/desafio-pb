using System.Net;
using System.Text.Json;
using DesafioPB.Common.Notifications;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPB.CadastroCliente.Filters;

public class AfterEndpointExecution : IEndpointFilter
{
  private readonly INotificationContext notification;
  private readonly ILogger<AfterEndpointExecution> logger;

  public AfterEndpointExecution(INotificationContext notification, ILogger<AfterEndpointExecution> logger)
  {
    this.notification = notification;
    this.logger = logger;
  }

  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    var result = await next(context);

    if ((result is BadRequest<string> dp) && notification.Any())
    {
      var problemDetails = new ProblemDetails
      {
        Status = HttpStatusCode.BadRequest.GetHashCode(),
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        Title = dp.Value,
        Detail = JsonSerializer.Serialize(notification.ReadOnlyList().Select(n => n.Message).ToList()),
        Instance = context.HttpContext.Request.Path
      };
      logger.LogInformation("A bad request was made with the following resoppnse: {Response}", problemDetails);

      return Results.Json(problemDetails);
    }
    return result;
  }
}
