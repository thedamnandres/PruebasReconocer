using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class MarketplacePremiosController : BaseCrudController<MarketplacePremio, MarketplacePremioResponse>
{
    public MarketplacePremiosController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<MarketplacePremioResponse>>> GetAll()
    {
        var items = await _context.MarketplacePremios
            .Include(p => p.Organizacion)
            .ToListAsync();

        return Ok(_mapper.Map<List<MarketplacePremioResponse>>(items));
    }

    public override async Task<ActionResult<MarketplacePremioResponse>> GetById(object id)
    {
        var item = await _context.MarketplacePremios
            .Include(p => p.Organizacion)
            .FirstOrDefaultAsync(p => p.PremioId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<MarketplacePremioResponse>(item));
    }
}
