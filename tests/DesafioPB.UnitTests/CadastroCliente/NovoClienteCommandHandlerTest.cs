using DesafioPB.CadastroCliente.Clientes.Commands;
using DesafioPB.CadastroCliente.Commands;
using DesafioPB.CadastroCliente.Commands.Validators;
using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DesafioPB.UnitTests.CadastroCliente;

[SuppressMessage("Usage", "NUnit1032", Justification = "Test methods can be non-public.")]
public class NovoClienteCommandHandlerTest
{
    private readonly Mock<IMessageBus> bus = new();
    private readonly Mock<IServiceProvider> serviceProvider = new();
    private readonly Mock<INotificationContext> notification = new();
    private CadastroClienteDbContext context;
    private Mock<IValidator<NovoClienteCommand>> novoClientValidator = new();

    private IRequestHandler<NovoClienteCommand, (bool, int?)> commandHandler;

    [SetUp]
    public void Setup()
    {
        context = GetInMemoryDbContext();
        serviceProvider.Setup(s => s.GetService(typeof(IValidator<NovoClienteCommand>))).Returns(novoClientValidator.Object);
        commandHandler = new NovoClienteCommandHandler(bus.Object, serviceProvider.Object, 
            notification.Object, context);
    }

    [Test]
    public async Task Handle_ShouldReturn_Status_False_If_Validation_Failed()
    {
        NovoClienteCommand request = new("", "", "", "");
        novoClientValidator.Setup(v => v.Validate(It.IsAny<NovoClienteCommand>()))
            .Returns(new ValidationResult(new[] { new ValidationFailure("PropName", "messageError") }));
        var (status, _) = await commandHandler.Handle(request, new CancellationToken());
        Assert.IsFalse(status);
    }

    [Test]
    public async Task Handle_ShouldReturn_Status_False_If_Validation_Passed()
    {
        NovoClienteCommand request = new("", "", "", "");
        novoClientValidator.Setup(v => v.Validate(It.IsAny<NovoClienteCommand>()))
            .Returns(new ValidationResult());
        var (status, _) = await commandHandler.Handle(request, new CancellationToken());
        Assert.IsTrue(status);
    }

    [Test]
    public async Task Handle_ShouldCall_Publish_When_Validation_Passed()
    {
        NovoClienteCommand request = new("", "", "", "");
        novoClientValidator.Setup(v => v.Validate(It.IsAny<NovoClienteCommand>()))
            .Returns(new ValidationResult());
        var (status, _) = await commandHandler.Handle(request, new CancellationToken());

        bus.Verify(n => n.PublishAsync(It.IsAny<CadastroClienteAddedEvent>(), default), Times.Once);
    }

    private CadastroClienteDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<CadastroClienteDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new CadastroClienteDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}
