using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletTransaccionesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WalletTransaccionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletTransaccion>>> GetAll()
        => await _context.WalletTransacciones.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletTransaccion>> GetById(int id)
    {
        var item = await _context.WalletTransacciones.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<WalletTransaccion>> Create(WalletTransaccion data)
    {
        _context.WalletTransacciones.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.TransaccionId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WalletTransaccion data)
    {
        if (id != data.TransaccionId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.WalletTransacciones.FindAsync(id);
        if (item is null) return NotFound();
        _context.WalletTransacciones.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
