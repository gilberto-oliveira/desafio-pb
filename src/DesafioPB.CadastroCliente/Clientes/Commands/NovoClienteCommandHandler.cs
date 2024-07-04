using DesafioPB.CadastroCliente.Commands;
using DesafioPB.CadastroCliente.Commands.Validators;
using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;
using FluentValidation;
using MediatR;

namespace DesafioPB.CadastroCliente.Clientes.Commands;

public class NovoClienteCommandHandler :
  IRequestHandler<NovoClienteCommand, (bool, int?)>
{
    private readonly IMessageBus bus;
    private readonly IServiceProvider serviceProvider;
    private readonly INotificationContext notification;
    private readonly CadastroClienteDbContext context;

    public NovoClienteCommandHandler(IMessageBus bus, IServiceProvider serviceProvider, INotificationContext notification, CadastroClienteDbContext context)
    {
        this.bus = bus;
        this.serviceProvider = serviceProvider;
        this.notification = notification;
        this.context = context;
    }

    public async Task<(bool, int?)> Handle(NovoClienteCommand request, CancellationToken token)
    {
        if (!IsValid(request)) return (false, null);

        Contexts.Entities.Cliente novoCliente = request;
        
        // TODO: lógica para criação de novo cliente

        context.Add(novoCliente);
        await context.SaveChangesAsync(token);

        await bus.PublishAsync(new CadastroClienteAddedEvent(novoCliente.Id, novoCliente.Nome, 
            novoCliente.Sobrenome, novoCliente.Cpf, novoCliente.Email), token);

        return (true, novoCliente.Id);
    }

    private bool IsValid(NovoClienteCommand request)
    {
        var validator = serviceProvider.GetService<IValidator<NovoClienteCommand>>()!;
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid) return true;

        notification.Add(validationResult.Errors.Select(er => new Notification(er.ErrorMessage)).ToArray());
        return false;
    }
}
