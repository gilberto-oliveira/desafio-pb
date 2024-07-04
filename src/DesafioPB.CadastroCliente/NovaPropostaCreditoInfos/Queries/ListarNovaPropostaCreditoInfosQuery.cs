using MediatR;

namespace DesafioPB.CadastroCliente.NovaPropostaCreditoInfos.Queries;

public record ListarNovaPropostaCreditoInfosQuery : IRequest<IEnumerable<ListarNovaPropostaCreditoInfosViewModel>>
{
  public ListarNovaPropostaCreditoInfosQuery(int clienteId, string? clienteCpf, decimal? limite, string? mensagem)
  {
    ClienteId = clienteId;
    ClienteCpf = clienteCpf;
    Limite = limite;
    Mensagem = mensagem;
  }

  public const string CacheKey = nameof(ListarNovaPropostaCreditoInfosQuery);
  public int ClienteId { get; set; }
  public string? ClienteCpf { get; set; }
  public decimal? Limite { get; set; }
  public string? Mensagem { get; set; }
}
