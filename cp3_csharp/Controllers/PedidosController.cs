using cp3_csharp.Data;
using cp3_csharp.Models.Database;
using cp3_csharp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cp3_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly LojaContext _db;
    public PedidosController(LojaContext db)
    {
        _db = db;
    }

    [HttpGet("todos")]
    public ActionResult<IEnumerable<Pedido>> GetTodas([FromQuery] DateTime? dia = null)
    {
        var lista = _db.Pedidos.AsNoTracking().ToList();
        return Ok(lista);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<object>> GetPorId(int id)
    {
        var p = await _db.Pedidos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();
        var itens = await _db.ItensPedido.AsNoTracking().Where(i => i.PedidoId == id).ToListAsync();
        return Ok(new { pedido = p, itens });
    }

    [HttpPost("novo")]
    public async Task<ActionResult> Novo([FromBody] PedidoCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CpfCliente))
            return BadRequest("CPF obrigatório.");

        var entity = new Pedido
        {
            CpfCliente = dto.CpfCliente,
            DataPedido = dto.DataPedido ?? DateTime.Now,
            Status = dto.Status ?? Status.Pendente
        };

        _db.Pedidos.Add(entity);
        await _db.SaveChangesAsync();

        return Created($"/api/pedidos/{entity.Id}", entity);
    }

    [HttpPost("atualiza/{id:int}")]
    public async Task<ActionResult> Atualiza(int id, [FromBody] PedidoUpdateDTO dto)
    {
        var entity = await _db.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null) return NotFound();

        entity.Status = dto.Status;

        await _db.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPost("remove/{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var entity = await _db.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null) return NotFound();

        _db.Pedidos.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
