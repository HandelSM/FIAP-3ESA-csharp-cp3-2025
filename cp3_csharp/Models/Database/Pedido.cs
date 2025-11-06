namespace cp3_csharp.Models.Database;

public class Pedido
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; }
    public string CpfCliente { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    Pendente,
    Processando,
    Enviado,
    Entregue,
    Cancelado
}
