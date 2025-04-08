using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizacionesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrganizacionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Organizacion>>> GetAll()
        => await _context.Organizaciones.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Organizacion>> GetById(int id)
    {
        var item = await _context.Organizaciones.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Organizacion>> Create(Organizacion data)
    {
        _context.Organizaciones.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.OrganizacionId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Organizacion data)
    {
        if (id != data.OrganizacionId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Organizaciones.FindAsync(id);
        if (item is null) return NotFound();
        _context.Organizaciones.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}