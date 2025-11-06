using System.ComponentModel.DataAnnotations.Schema;

namespace cp3_csharp.Models.Database;

[Table("ITENS_PEDIDO")]
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int CamisaId { get; set; }
}
