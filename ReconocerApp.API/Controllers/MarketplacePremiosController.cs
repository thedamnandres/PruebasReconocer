using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketplacePremiosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MarketplacePremiosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarketplacePremio>>> GetAll(){
        List<ReconocerApp.API.Models.Responses.MarketplacePremio> premios = new List<ReconocerApp.API.Models.Responses.MarketplacePremio>();
        var items = await _context.MarketplacePremios.ToListAsync();
        foreach (var item in items)
        {
            var organizacion = await _context.Organizaciones.FindAsync(item.OrganizacionId);
            if (organizacion != null)
            {
                premios.Add(new ReconocerApp.API.Models.Responses.MarketplacePremio
                {
                    PremioId = item.PremioId,
                    OrganizacionId = item.OrganizacionId,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    CostoWallet = item.CostoWallet,
                    ImagenUrl = item.ImagenUrl,
                    CantidadActual = item.CantidadActual,
                    UltimaActualizacion = item.UltimaActualizacion,
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
        return Ok(premios);
        
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<MarketplacePremio>> GetById(int id)
    {
        var item = await _context.MarketplacePremios.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<MarketplacePremio>> Create(MarketplacePremio data)
    {
        _context.MarketplacePremios.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.PremioId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MarketplacePremio data)
    {
        if (id != data.PremioId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.MarketplacePremios.FindAsync(id);
        if (item is null) return NotFound();
        _context.MarketplacePremios.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}