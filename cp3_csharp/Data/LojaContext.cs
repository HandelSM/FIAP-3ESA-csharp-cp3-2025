using cp3_csharp.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace cp3_csharp.Data;

public class LojaContext : DbContext
{
    public LojaContext(DbContextOptions<LojaContext> options) : base(options) { }

    public DbSet<Banda> Bandas => Set<Banda>();
    public DbSet<Camisa> Camisas => Set<Camisa>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
}
