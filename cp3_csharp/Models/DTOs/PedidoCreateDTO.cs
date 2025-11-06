using cp3_csharp.Models.Database;

namespace cp3_csharp.Models.DTOs;


public class PedidoCreateDTO
{
    public string CpfCliente { get; set; }
    public DateTime? DataPedido { get; set; }
    public Status? Status { get; set; }
}

