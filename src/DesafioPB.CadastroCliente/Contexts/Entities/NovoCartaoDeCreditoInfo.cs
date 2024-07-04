namespace DesafioPB.CadastroCliente.Contexts.Entities;

public class NovoCartaoDeCreditoInfo
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public required string Numero { get; set; }
    public required int Cvv { get; set; }
    public required string Validade { get; set; }
    public required string Mensagem { get; set; }
}