using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletCategoriasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WalletCategoriasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletCategoria>>> GetAll()
        => await _context.WalletCategorias.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletCategoria>> GetById(int id)
    {
        var item = await _context.WalletCategorias.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<WalletCategoria>> Create(WalletCategoria data)
    {
        _context.WalletCategorias.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.CategoriaId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WalletCategoria data)
    {
        if (id != data.CategoriaId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.WalletCategorias.FindAsync(id);
        if (item is null) return NotFound();
        _context.WalletCategorias.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
