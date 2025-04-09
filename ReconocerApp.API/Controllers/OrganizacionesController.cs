using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class OrganizacionesController : BaseCrudController<Organizacion, OrganizacionResponse>
{
    public OrganizacionesController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }
}
