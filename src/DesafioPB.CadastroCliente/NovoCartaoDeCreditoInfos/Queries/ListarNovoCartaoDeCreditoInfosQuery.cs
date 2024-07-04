using MediatR;

namespace DesafioPB.CadastroCliente.NovoCartaoDeCreditoInfos.Queries;

public record ListarNovoCartaoDeCreditoInfosQuery : IRequest<IEnumerable<ListarNovoCartaoDeCreditoInfosViewModel>>
{
  public ListarNovoCartaoDeCreditoInfosQuery(int clienteId, string? numero, string? cvv, string? validade, string? mensagem)
  {
    ClienteId = clienteId;
    Numero = numero;
    Cvv = cvv;
    Validade = validade;
    Mensagem = mensagem;
  }

  public const string CacheKey = nameof(ListarNovoCartaoDeCreditoInfosQuery);
  public int ClienteId { get; set; }
  public string? Numero { get; set; }
  public string? Cvv { get; set; }
  public string? Validade { get; set; }
  public string? Mensagem { get; set; }
}
