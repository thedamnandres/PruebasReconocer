using Microsoft.EntityFrameworkCore;
using ReconocerApp.API.Data;
using ReconocerApp.API.Mappings; // 👈 Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);
// Habilitar logs de consola
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

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

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "Welcome to the API!");
app.Run();

