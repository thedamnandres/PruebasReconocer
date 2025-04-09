using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Mappings; // 👈 Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile)); // 👈 ¡Aquí está el cambio!

// Configurar DbContext dinámicamente
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (databaseProvider == "Sqlite")
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    throw new Exception("Unsupported database provider specified in appsettings.json");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
