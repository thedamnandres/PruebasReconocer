using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventarioTransaccionesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public InventarioTransaccionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventarioTransaccion>>> GetAll()
        => await _context.InventarioTransacciones.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<InventarioTransaccion>> GetById(int id)
    {
        var item = await _context.InventarioTransacciones.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<InventarioTransaccion>> Create(InventarioTransaccion data)
    {
        _context.InventarioTransacciones.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.TransaccionId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, InventarioTransaccion data)
    {
        if (id != data.TransaccionId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.InventarioTransacciones.FindAsync(id);
        if (item is null) return NotFound();
        _context.InventarioTransacciones.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}