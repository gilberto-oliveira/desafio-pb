using System.Diagnostics.CodeAnalysis;
using DesafioPB.PropostaCredito.Contexts;
using DesafioPB.PropostaCredito.Messaging;
using DesafioPB.PropostaCredito.Services;
using DesafioPB.PropostaCredito.Services.Validators;

var builder = WebApplication.CreateBuilder(args);
                                                                       
builder.Logging.AddConsole();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<NovaPropostaCreditoValidator>();

builder.Services.AddScoped<IService, NovaPropostaCreditoService>();

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
             