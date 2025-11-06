using cp3_csharp.Data;
using cp3_csharp.Models.Database;
using cp3_csharp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cp3_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BandasController : ControllerBase
{
    private readonly LojaContext _db;

    public BandasController(LojaContext db)
    {
        _db = db;
    }

    [HttpGet("todas")]
    public async Task<ActionResult<IEnumerable<Banda>>> GetAll()
    {
        var lista = await _db.Bandas.AsNoTracking()
                                    .ToListAsync();
        return Ok(lista);
    }

    [HttpPost("nova")]
    public async Task<ActionResult> Create([FromBody] BandaCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) ||
            string.IsNullOrWhiteSpace(dto.Pais) ||
            string.IsNullOrWhiteSpace(dto.Genero))
            return BadRequest("Nome, País e Gênero são obrigatórios.");

        var exists = await _db.Bandas.AsNoTracking().AnyAsync(b => b.Nome == dto.Nome);
        if (exists) return Conflict("Já existe uma banda com esse nome.");

        var entity = new Banda { Nome = dto.Nome, Pais = dto.Pais, Genero = dto.Genero};
        _db.Bandas.Add(entity);
        await _db.SaveChangesAsync();

        return Created();
    }


    [HttpPost("atualiza/{id:int}")]
    public async Task<ActionResult> Atualiza(int id, [FromBody] BandaUpdateDTO dto)
    {
        var entity = await _db.Bandas.FirstOrDefaultAsync(b => b.Id == id);
        if (entity is null) return NotFound();

        var nomeAlvo = dto.Nome?.Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(nomeAlvo))
        {
            var jaExiste = await _db.Bandas.AsNoTracking()
                              .AnyAsync(b => b.Nome == nomeAlvo && b.Id != id);
            if (jaExiste) return Conflict("Já existe uma banda com esse nome.");
            entity.Nome = nomeAlvo;
        }

        if (!string.IsNullOrWhiteSpace(dto.Pais)) entity.Pais = dto.Pais.Trim();
        if (!string.IsNullOrWhiteSpace(dto.Genero)) entity.Genero = dto.Genero.Trim();

        await _db.SaveChangesAsync();
        return Ok(entity);
    }


    [HttpPost("remove/{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var entity = await _db.Bandas.FirstOrDefaultAsync(b => b.Id == id);
        if (entity is null) return NotFound();

        _db.Bandas.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
