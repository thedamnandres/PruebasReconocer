using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;

namespace ReconocerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColaboradoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ColaboradoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Colaborador>>> GetAll(){
        List<ReconocerApp.API.Models.Responses.ColaboradorResponse> colaboradores = new List<ReconocerApp.API.Models.Responses.ColaboradorResponse>();
        var items = await _context.Colaboradores.ToListAsync();
        foreach (var item in items)
        {
            var organizacion = await _context.Organizaciones.FindAsync(item.OrganizacionId);
            if (organizacion != null)
            {
                colaboradores.Add(new ReconocerApp.API.Models.Responses.ColaboradorResponse
                {
                    ColaboradorId = item.ColaboradorId,
                    OrganizacionId = item.OrganizacionId,
                    ExcepcionConfiguracion = item.ExcepcionConfiguracion,
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
        return Ok(colaboradores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Colaborador>> GetById(string id)
    {
        var item = await _context.Colaboradores.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Colaborador>> Create(Colaborador data)
    {
        _context.Colaboradores.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = data.ColaboradorId }, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Colaborador data)
    {
        if (id != data.ColaboradorId) return BadRequest();
        _context.Entry(data).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var item = await _context.Colaboradores.FindAsync(id);
        if (item is null) return NotFound();
        _context.Colaboradores.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}