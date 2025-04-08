using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketplaceComprasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MarketplaceComprasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarketplaceCompra>>> GetAll()
        => await _context.MarketplaceCompras.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<MarketplaceCompra>> GetById(int id)
    {
        var item = await _context.MarketplaceCompras.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<MarketplaceCompra>> Create(MarketplaceCompra data)
    {
        _context.MarketplaceCompras.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.CompraId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MarketplaceCompra data)
    {
        if (id != data.CompraId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.MarketplaceCompras.FindAsync(id);
        if (item is null) return NotFound();
        _context.MarketplaceCompras.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}