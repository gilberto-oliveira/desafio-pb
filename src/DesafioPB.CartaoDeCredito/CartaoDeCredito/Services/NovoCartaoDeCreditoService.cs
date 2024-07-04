using DesafioPB.CartaoDeCredito.Contexts;
using DesafioPB.CartaoDeCredito.Services.Validators;
using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;
using System;
using System.Text.Json;

namespace DesafioPB.CartaoDeCredito.Services;

public interface IService 
{
    Task<(bool, int?)> Handle(PropostaCreditoAddedEvent request, CancellationToken token);
}

public class NovoCartaoDeCreditoService: IService
{
    private readonly IMessageBus bus;
    private readonly IServiceProvider serviceProvider;
    private readonly INotificationContext notification;
    private readonly CartaoDeCreditoDbContext context;

    public NovoCartaoDeCreditoService(IMessageBus bus, IServiceProvider serviceProvider, INotificationContext notification, CartaoDeCreditoDbContext context)
    {
        this.bus = bus;
        this.serviceProvider = serviceProvider;
        this.notification = notification;
        this.context = context;
    }

    public async Task<(bool, int?)> Handle(PropostaCreditoAddedEvent request, CancellationToken token)
    {
        if (!IsValid(request))
        {
            await bus.PublishAsync(new CartaoDeCreditoInfoEvent(request.clienteId, "",
            0, "", "Nao e possivel gerar o cartao de credito! Proposta invalida! Detalhes: " + JsonSerializer.Serialize(notification.ReadOnlyList().Select(n => n.Message).ToList())), token);
            return (false, null);
        }

        // TODO: lógica para criação do cartao de credito
        var numeroCartao = GerarNumeroDoCartao("8", 14);
        var cvv = new Random().Next(100, 1000);
        var validade = DateTime.Now.AddYears(5).ToString("MM/yy");

        Contexts.Entities.CartaoDeCredito novoCartao = new() { 
            ClienteId = request.clienteId, Numero = numeroCartao,
            Cvv = cvv, Validade = validade
        };

        context.Add(novoCartao);
        await context.SaveChangesAsync(token);

        await bus.PublishAsync(new CartaoDeCreditoInfoEvent(novoCartao.ClienteId, novoCartao.Numero,
            novoCartao.Cvv, novoCartao.Validade, "Cartao de credito gerado com sucesso! Aproveite!"), token);

        return (true, novoCartao.Id);
    }

    private bool IsValid(PropostaCreditoAddedEvent request)
    {
        var validator = serviceProvider.GetService<NovoCartaoDeCreditoValidator>()!;
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid) return true;

        notification.Add(validationResult.Errors.Select(er => new Notification(er.ErrorMessage)).ToArray());
        return false;
    }

    private string GerarNumeroDoCartao(string prefix, int length)
    {
        var cardNumber = prefix;

        // Gerar dígitos aleatórios até a penúltima posição
        while (cardNumber.Length < length - 1)
        {
            cardNumber += new Random().Next(0, 10).ToString();
        }

        // Calcular o dígito verificador usando o algoritmo de Luhn
        var checkDigit = CalcularDigitoVerificador(cardNumber);
        cardNumber += checkDigit;

        return cardNumber;
    }

    private int CalcularDigitoVerificador(string number)
    {
        int sum = 0;
        bool alternate = true;

        // Percorrer os dígitos de trás para frente
        for (int i = number.Length - 1; i >= 0; i--)
        {
            int n = int.Parse(number[i].ToString());
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                {
                    n -= 9;
                }
            }
            sum += n;
            alternate = !alternate;
        }

        return (10 - (sum % 10)) % 10;
    }
}
