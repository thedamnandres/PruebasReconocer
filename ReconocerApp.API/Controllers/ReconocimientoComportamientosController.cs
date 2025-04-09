using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class ReconocimientoComportamientosController : BaseCrudController<ReconocimientoComportamiento, ReconocimientoComportamientoResponse>
{
    public ReconocimientoComportamientosController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<ReconocimientoComportamientoResponse>>> GetAll()
    {
        var items = await _context.ReconocimientoComportamientos
            .Include(rc => rc.Comportamiento)
            .Include(rc => rc.Reconocimiento)
            .ToListAsync();

        return Ok(_mapper.Map<List<ReconocimientoComportamientoResponse>>(items));
    }

    public override async Task<ActionResult<ReconocimientoComportamientoResponse>> GetById(object id)
    {
        var item = await _context.ReconocimientoComportamientos
            .Include(rc => rc.Comportamiento)
            .Include(rc => rc.Reconocimiento)
            .FirstOrDefaultAsync(rc => rc.Id == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<ReconocimientoComportamientoResponse>(item));
    }
}
