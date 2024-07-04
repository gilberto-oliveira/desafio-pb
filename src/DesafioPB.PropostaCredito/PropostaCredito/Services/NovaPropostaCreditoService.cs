using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;
using DesafioPB.PropostaCredito.Contexts;
using DesafioPB.PropostaCredito.Services.Validators;
using System.Text.Json;

namespace DesafioPB.PropostaCredito.Services;

public interface IService 
{
    Task<(bool, int?)> Handle(CadastroClienteAddedEvent request, CancellationToken token);
}

public class NovaPropostaCreditoService: IService
{
    private readonly IMessageBus bus;
    private readonly IServiceProvider serviceProvider;
    private readonly INotificationContext notification;
    private readonly PropostaCreditoDbContext context;

    public NovaPropostaCreditoService(IMessageBus bus, IServiceProvider serviceProvider, INotificationContext notification, PropostaCreditoDbContext context)
    {
        this.bus = bus;
        this.serviceProvider = serviceProvider;
        this.notification = notification;
        this.context = context;
    }

    public async Task<(bool, int?)> Handle(CadastroClienteAddedEvent request, CancellationToken token)
    {
        Contexts.Entities.PropostaCredito proposta = new() { ClienteCpf = request.cpf, ClienteId = request.id, Limite = 0 };

        // calcula limite inicial do cliente
        var limiteInicial = await CalcularLimiteInicial(proposta);
        
        if (!IsValid(request))
        {
            await bus.PublishAsync(new PropostaCreditoInfoEvent(request.id, request.cpf,
            0, "Proposta invalida! Detalhes: " + JsonSerializer.Serialize(notification.ReadOnlyList().Select(n => n.Message).ToList())), token);
            return (false, null);
        }

        if (limiteInicial <= 0)
        {
            await bus.PublishAsync(new PropostaCreditoInfoEvent(request.id, request.cpf,
            0, "Proposta invalida! Detalhes: O cliente nao possui limite inicial disponivel!"), token);
            return (false, null);
        }

        // TODO: lógica para criação de proposta do cliente
        proposta.Limite = limiteInicial;

        context.Add(proposta);
        await context.SaveChangesAsync(token);

        await bus.PublishAsync(new PropostaCreditoAddedEvent(proposta.ClienteId, proposta.ClienteCpf,
            proposta.Limite, "Proposta aprovada com sucesso! Em breve, teremos mais informacoes sobre o seu cartao de credito."), token);

        await bus.PublishAsync(new PropostaCreditoInfoEvent(proposta.ClienteId, proposta.ClienteCpf,
            proposta.Limite, "Proposta aprovada com sucesso! Em breve, teremos mais informacoes sobre o seu cartao de credito."), token);

        return (true, proposta.Id);
    }

    private bool IsValid(CadastroClienteAddedEvent request)
    {
        var validator = serviceProvider.GetService<NovaPropostaCreditoValidator>()!;
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid) return true;

        notification.Add(validationResult.Errors.Select(er => new Notification(er.ErrorMessage)).ToArray());
        return false;
    }

    private async Task<decimal> CalcularLimiteInicial(Contexts.Entities.PropostaCredito item)
    {
        if (item.ClienteCpf.StartsWith("01")) return await Task.FromResult(0);
        return await Task.FromResult(new Random().Next(0, 5001));
    }
}
