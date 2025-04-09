using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class MarketplaceComprasController : BaseCrudController<MarketplaceCompra, MarketplaceCompraResponse>
{
    public MarketplaceComprasController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<MarketplaceCompraResponse>>> GetAll()
    {
        var items = await _context.MarketplaceCompras
            .Include(c => c.Colaborador)
            .Include(c => c.Premio)
            .ToListAsync();

        return Ok(_mapper.Map<List<MarketplaceCompraResponse>>(items));
    }

    public override async Task<ActionResult<MarketplaceCompraResponse>> GetById(object id)
    {
        var item = await _context.MarketplaceCompras
            .Include(c => c.Colaborador)
            .Include(c => c.Premio)
            .FirstOrDefaultAsync(c => c.CompraId == (int)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<MarketplaceCompraResponse>(item));
    }
}
