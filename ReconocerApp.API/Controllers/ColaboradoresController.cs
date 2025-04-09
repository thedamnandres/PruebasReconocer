using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class ColaboradoresController : BaseCrudController<Colaborador, ColaboradorResponse>
{
    public ColaboradoresController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }

    public override async Task<ActionResult<IEnumerable<ColaboradorResponse>>> GetAll()
    {
        var items = await _context.Colaboradores
            .Include(c => c.Organizacion)
            .ToListAsync();

        return Ok(_mapper.Map<List<ColaboradorResponse>>(items));
    }

    public override async Task<ActionResult<ColaboradorResponse>> GetById(object id)
    {
        var item = await _context.Colaboradores
            .Include(c => c.Organizacion)
            .FirstOrDefaultAsync(c => c.ColaboradorId == (string)id);

        if (item == null) return NotFound();
        return Ok(_mapper.Map<ColaboradorResponse>(item));
    }
}
