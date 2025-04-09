using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class ReconocimientosController : BaseCrudController<Reconocimiento, ReconocimientoResponse>
{
    public ReconocimientosController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<ReconocimientoResponse>>> GetAll()
    {
        var items = await _context.Reconocimientos
            .Include(r => r.Colaborador)
            .ToListAsync();

        return Ok(_mapper.Map<List<ReconocimientoResponse>>(items));
    }

    public override async Task<ActionResult<ReconocimientoResponse>> GetById(object id)
    {
        var item = await _context.Reconocimientos
            .Include(r => r.Colaborador)
            .FirstOrDefaultAsync(r => r.ReconocimientoId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<ReconocimientoResponse>(item));
    }
}
