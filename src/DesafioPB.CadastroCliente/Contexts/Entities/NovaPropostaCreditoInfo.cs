namespace DesafioPB.CadastroCliente.Contexts.Entities;

public class NovaPropostaCreditoInfo
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public required string ClienteCpf { get; set; }
    public required decimal Limite { get; set; }
    public required string Mensagem { get; set; }
}