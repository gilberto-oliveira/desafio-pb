using DesafioPB.CadastroCliente.Commands;
using DesafioPB.CadastroCliente.Contexts;
using DesafioPB.CartaoDeCredito.Contexts;
using DesafioPB.CartaoDeCredito.Services;
using DesafioPB.Common.Messaging;
using DesafioPB.Common.Notifications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DesafioPB.UnitTests.CartaoDeCredito;

[SuppressMessage("Usage", "NUnit1032", Justification = "Test methods can be non-public.")]
public class NovoCartaoDeCreditoServiceTest
{
    private readonly Mock<IMessageBus> bus = new();
    private readonly Mock<IServiceProvider> serviceProvider = new();
    private readonly Mock<INotificationContext> notification = new();
    private CartaoDeCreditoDbContext context;

    private IService service;

    [SetUp]
    public void Setup()
    {
        context = GetInMemoryDbContext();
        service = new NovoCartaoDeCreditoService(bus.Object, serviceProvider.Object, notification.Object, context);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    private CartaoDeCreditoDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<CartaoDeCreditoDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new CartaoDeCreditoDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}
