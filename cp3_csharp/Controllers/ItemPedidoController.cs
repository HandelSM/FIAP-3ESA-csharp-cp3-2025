using cp3_csharp.Data;
using cp3_csharp.Models.Database;
using cp3_csharp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cp3_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensPedidoController : ControllerBase
{
    private readonly LojaContext _db;
    public ItensPedidoController(LojaContext db)
    {
        _db = db;
    }

    [HttpGet("todos")]
    public ActionResult<IEnumerable<ItemPedido>> GetTodos()
    {
        var result = _db.ItensPedido.AsNoTracking().ToList();
        return Ok(result);
    }

    [HttpPost("novo")]
    public async Task<ActionResult> Novo([FromBody] ItemPedidoCreateDTO dto)
    {
        var pedidoExiste = await _db.Pedidos.AsNoTracking().AnyAsync(p => p.Id == dto.PedidoId);
        if (!pedidoExiste) return BadRequest($"Pedido {dto.PedidoId} não encontrado.");

        var camisaExiste = await _db.Camisas.AsNoTracking().AnyAsync(c => c.Id == dto.CamisaId);
        if (!camisaExiste) return BadRequest($"Camisa {dto.CamisaId} não encontrada.");

        var entity = new ItemPedido { PedidoId = dto.PedidoId, CamisaId = dto.CamisaId };
        _db.ItensPedido.Add(entity);

        await _db.SaveChangesAsync();

        return Created($"/api/itenspedido/{entity.Id}", entity);
    }

    [HttpPost("atualiza/{id:int}")]
    public async Task<ActionResult> Atualiza(int id, [FromBody] ItemPedidoUpdateDTO dto)
    {
        var entity = await _db.ItensPedido.FirstOrDefaultAsync(i => i.Id == id);
        if (entity is null) return NotFound();

        var camisaExiste = await _db.Camisas.AsNoTracking().AnyAsync(c => c.Id == dto.CamisaId);
        if (!camisaExiste) return BadRequest($"Camisa {dto.CamisaId} não encontrada.");

        entity.CamisaId = dto.CamisaId;

        try { await _db.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            return Conflict("Conflito ao salvar (possível duplicidade de Camisa no mesmo Pedido).");
        }

        return Ok(entity);
    }

    [HttpPost("remove/{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var entity = await _db.ItensPedido.FirstOrDefaultAsync(i => i.Id == id);
        if (entity is null) return NotFound();

        _db.ItensPedido.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
