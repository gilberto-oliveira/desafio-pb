using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DesafioPB.CadastroCliente;
using DesafioPB.CadastroCliente.Commands;
using DesafioPB.CadastroCliente.Commands.Validators;
using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CadastroCliente.Messaging;
using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries;
using DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries.Validations;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IValidator<NovoClienteCommand> , NovoClienteValidator>();
builder.Services.AddScoped<IValidator<ListarNovoCartaoDeCreditoInfosQuery>, ListarNovoCartaoDeCreditoInfosValidator>();

builder.Services.AddContextServices();

builder.Services.AddMessaging();

builder.Services.AddMessagingConsumer(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/");

app.MapGroup("/api/cliente")
.MapClientesApiV1()
.WithTags("CadastroCliente.Api");

app.Run();

namespace DesafioPB.CadastroCliente
{
    [ExcludeFromCodeCoverage]
    public partial class Program
    { }
}
