using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletSaldosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WalletSaldosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletSaldo>>> GetAll()
        => await _context.WalletSaldos.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletSaldo>> GetById(int id)
    {
        var item = await _context.WalletSaldos.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<WalletSaldo>> Create(WalletSaldo data)
    {
        _context.WalletSaldos.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.WalletSaldoId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WalletSaldo data)
    {
        if (id != data.WalletSaldoId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.WalletSaldos.FindAsync(id);
        if (item is null) return NotFound();
        _context.WalletSaldos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
