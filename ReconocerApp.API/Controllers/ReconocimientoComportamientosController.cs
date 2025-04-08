using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReconocimientoComportamientosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReconocimientoComportamientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReconocimientoComportamiento>>> GetAll()
        => await _context.ReconocimientoComportamientos.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<ReconocimientoComportamiento>> GetById(int id)
    {
        var item = await _context.ReconocimientoComportamientos.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<ReconocimientoComportamiento>> Create(ReconocimientoComportamiento data)
    {
        _context.ReconocimientoComportamientos.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReconocimientoComportamiento data)
    {
        if (id != data.Id) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ReconocimientoComportamientos.FindAsync(id);
        if (item is null) return NotFound();
        _context.ReconocimientoComportamientos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}