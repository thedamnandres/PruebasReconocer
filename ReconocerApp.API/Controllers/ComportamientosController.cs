using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class ComportamientosController : BaseCrudController<Comportamiento, ComportamientoResponse>
{
    public ComportamientosController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<ComportamientoResponse>>> GetAll()
    {
        var items = await _context.Comportamientos
            .Include(c => c.Organizacion)
            .ToListAsync();

        return Ok(_mapper.Map<List<ComportamientoResponse>>(items));
    }

    public override async Task<ActionResult<ComportamientoResponse>> GetById(object id)
    {
        var item = await _context.Comportamientos
            .Include(c => c.Organizacion)
            .FirstOrDefaultAsync(c => c.ComportamientoId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<ComportamientoResponse>(item));
    }
}
