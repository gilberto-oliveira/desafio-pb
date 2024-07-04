using EasyNetQ;

namespace DesafioPB.Common.Messaging;

public abstract record BaseEventMessage
{
  public const string ExchangeName = "DesafioPB";
  public DateTime Timestamp => DateTime.Now;
  public string MessageType => GetType().Name;
}

[Queue(nameof(CadastroClienteAddedEvent), ExchangeName = "DesafioPB.CadastroCliente.AddedEvent")]
public record CadastroClienteAddedEvent(int id, string nome, string? sobrenome, string cpf, string email) : BaseEventMessage;

[Queue(nameof(CartaoDeCreditoAddedEvent), ExchangeName = "DesafioPB.CartaoDeCredito.AddedEvent")]
public record CartaoDeCreditoAddedEvent(int clienteId, string numero, string cvv, string validade) : BaseEventMessage;

[Queue(nameof(CartaoDeCreditoInfoEvent), ExchangeName = "DesafioPB.CartaoDeCredito.InfoEvent")]
public record CartaoDeCreditoInfoEvent(int clienteId, string numero, int cvv, string validade, string mensagem) : BaseEventMessage;

[Queue(nameof(PropostaCreditoAddedEvent), ExchangeName = "DesafioPB.PropostaCredito.AddedEvent")]
public record PropostaCreditoAddedEvent(int clienteId, string clienteCpf, decimal limite, string mensagem) : BaseEventMessage;

[Queue(nameof(PropostaCreditoInfoEvent), ExchangeName = "DesafioPB.PropostaCredito.InfoEvent")]
public record PropostaCreditoInfoEvent(int clienteId, string clienteCpf, decimal limite, string mensagem) : BaseEventMessage;