using System.Diagnostics.CodeAnalysis;
using System.Net;
using DesafioPB.CadastroCliente.Filters;
using DesafioPB.CadastroCliente.Clientes.Queries;
using DesafioPB.CadastroCliente.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries;
using DesafioPB.CadastroCliente.NovaPropostaCreditoInfos.Queries;

namespace DesafioPB.CadastroCliente;

[ExcludeFromCodeCoverage]
public static class EndpointsV1
{
  public static RouteGroupBuilder MapClientesApiV1(this RouteGroupBuilder group)
  {
    group
      .MapGet("/", ListarClientesAsync)
      .WithDescription("Retorna uma lista com todos os clientes")
      .Produces<List<ListarClientesViewModel>>(HttpStatusCode.OK.GetHashCode())
      .Produces<ProblemDetails>(HttpStatusCode.BadRequest.GetHashCode())
      .AddEndpointFilter<AfterEndpointExecution>();

    group
      .MapPost("/", NovoClienteAsync)
      .WithDescription("Cria um novo cliente")
      .Produces(HttpStatusCode.NoContent.GetHashCode())
      .Produces<ProblemDetails>(HttpStatusCode.BadRequest.GetHashCode())
      .AddEndpointFilter<AfterEndpointExecution>();

    group
      .MapGet("/nova-proposta-credito-info", ListarNovaPropostaCreditoInfosAsync)
      .WithDescription("Lista informações de retorno da API de Proposta de Crédito")
      .Produces(HttpStatusCode.NoContent.GetHashCode())
      .Produces<ProblemDetails>(HttpStatusCode.BadRequest.GetHashCode())
      .AddEndpointFilter<AfterEndpointExecution>();

    group
      .MapGet("/novo-cartao-de-credito-info", ListarNovoCartaoDeCreditoInfosAsync)
      .WithDescription("Lista informações de retorno da API de Cartao de Crédito")
      .Produces(HttpStatusCode.NoContent.GetHashCode())
      .Produces<ProblemDetails>(HttpStatusCode.BadRequest.GetHashCode())
      .AddEndpointFilter<AfterEndpointExecution>();

    return group.WithOpenApi();
  }

  private static async Task<IResult> ListarClientesAsync(IMediator mediator, string nome, string? sobrenome, string email, string cpf)
  {
    var response = await mediator.Send(new ListarClientesQuery(nome, sobrenome, email, cpf));

    if (response is null) return TypedResults.NotFound();

    return TypedResults.Ok(response);
  }

  private static async Task<IResult> NovoClienteAsync(IMediator mediator, [FromBody] NovoClienteCommand command)
  {
    var (success, id) = await mediator.Send(command);

    if (!success)
      return TypedResults.BadRequest("Nenhum cliente foi cadastrado, tente novamente mais tarde!");

    return TypedResults.Created($"/api/cliente/{id}");
  }

  private static async Task<IResult> ListarNovoCartaoDeCreditoInfosAsync(IMediator mediator, int clienteId, string? numero, string? cvv, string? validade, string? mensagem)
  {
    var response = await mediator.Send(new ListarNovoCartaoDeCreditoInfosQuery(clienteId, numero, cvv, validade, mensagem));

    if (response is null) return TypedResults.NotFound();

    return TypedResults.Ok(response);
  }

  private static async Task<IResult> ListarNovaPropostaCreditoInfosAsync(IMediator mediator, int clienteId, string? clienteCpf, decimal? limite, string? mensagem)
  {
    var response = await mediator.Send(new ListarNovaPropostaCreditoInfosQuery(clienteId, clienteCpf, limite, mensagem));

    if (response is null) return TypedResults.NotFound();

    return TypedResults.Ok(response);
  }
}