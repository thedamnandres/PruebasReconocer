using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class InventarioTransaccionesController : BaseCrudController<InventarioTransaccion, InventarioTransaccionResponse>
{
    public InventarioTransaccionesController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<InventarioTransaccionResponse>>> GetAll()
    {
        var items = await _context.InventarioTransacciones
            .Include(t => t.Premio)
            .ToListAsync();

        return Ok(_mapper.Map<List<InventarioTransaccionResponse>>(items));
    }

    public override async Task<ActionResult<InventarioTransaccionResponse>> GetById(object id)
    {
        var item = await _context.InventarioTransacciones
            .Include(t => t.Premio)
            .FirstOrDefaultAsync(t => t.TransaccionId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<InventarioTransaccionResponse>(item));
    }
}
