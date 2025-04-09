using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReconocerApp.API.Controllers.Base;
using ReconocerApp.API.Data;
using ReconocerApp.API.Models;
using ReconocerApp.API.Models.Responses;

namespace ReconocerApp.API.Controllers;

public class WalletCategoriasController : BaseCrudController<WalletCategoria, WalletCategoriaResponse>
{
    public WalletCategoriasController(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper) { }
}
