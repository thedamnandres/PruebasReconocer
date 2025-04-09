using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class WalletTransaccionesController : BaseCrudController<WalletTransaccion, WalletTransaccionResponse>
{
    public WalletTransaccionesController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<WalletTransaccionResponse>>> GetAll()
    {
        var items = await _context.WalletTransacciones
            .Include(t => t.Categoria)
            .Include(t => t.Colaborador)
            .ToListAsync();

        return Ok(_mapper.Map<List<WalletTransaccionResponse>>(items));
    }

    public override async Task<ActionResult<WalletTransaccionResponse>> GetById(object id)
    {
        var item = await _context.WalletTransacciones
            .Include(t => t.Categoria)
            .Include(t => t.Colaborador)
            .FirstOrDefaultAsync(t => t.TransaccionId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<WalletTransaccionResponse>(item));
    }
}
