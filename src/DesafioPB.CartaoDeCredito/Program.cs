using DesafioPB.CartaoDeCredito.Contexts;
using DesafioPB.CartaoDeCredito.Messaging;
using DesafioPB.CartaoDeCredito.Services;
using DesafioPB.CartaoDeCredito.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<NovoCartaoDeCreditoValidator>();

builder.Services.AddScoped<IService, NovoCartaoDeCreditoService>();

builder.Services.AddContextServices();

builder.Services.AddMessagingConsumer();

builder.Services.AddMessaging();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/");

app.Run();
