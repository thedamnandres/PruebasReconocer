using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComportamientosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ComportamientosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comportamiento>>> GetAll(){
        List<ReconocerApp.API.Models.Responses.ComportamientoResponse> comportamientos = new List<ReconocerApp.API.Models.Responses.ComportamientoResponse>();
        var items = await _context.Comportamientos.ToListAsync();
        foreach (var item in items)
        {
            var organizacion = await _context.Organizaciones.FindAsync(item.OrganizacionId);
            if (organizacion != null)
            {
                comportamientos.Add(new ReconocerApp.API.Models.Responses.ComportamientoResponse
                {
                    ComportamientoId = item.ComportamientoId,
                    OrganizacionId = item.OrganizacionId,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    WalletOtorgados = item.WalletOtorgados,
                    Organizacion = new ReconocerApp.API.Models.Responses.Org
                    {
                        OrganizacionId = organizacion.OrganizacionId,
                        Nombre = organizacion.Nombre,
                        Descripcion = organizacion.Descripcion,
                        DominioEmail = organizacion.DominioEmail,
                        Activa = organizacion.Activa
                    }
                });
            }
        }
        return Ok(comportamientos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comportamiento>> GetById(int id)
    {
        var item = await _context.Comportamientos.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Comportamiento>> Create(Comportamiento data)
    {
        _context.Comportamientos.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.ComportamientoId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Comportamiento data)
    {
        if (id != data.ComportamientoId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Comportamientos.FindAsync(id);
        if (item is null) return NotFound();
        _context.Comportamientos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}