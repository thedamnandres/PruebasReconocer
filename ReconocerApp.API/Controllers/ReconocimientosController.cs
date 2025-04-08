using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReconocimientosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReconocimientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reconocimiento>>> GetAll()
        => await _context.Reconocimientos.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Reconocimiento>> GetById(int id)
    {
        var item = await _context.Reconocimientos.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Reconocimiento>> Create(Reconocimiento data)
    {
        _context.Reconocimientos.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.ReconocimientoId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Reconocimiento data)
    {
        if (id != data.ReconocimientoId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Reconocimientos.FindAsync(id);
        if (item is null) return NotFound();
        _context.Reconocimientos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}