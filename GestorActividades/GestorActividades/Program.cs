using GestorActividades.Datos;
using GestorActividades.Repositorios.Interfaces;
using GestorActividades.Repositorios;
using Microsoft.EntityFrameworkCore;
using GestorActividades.Middleware;
using GestorActividades.Servicios;
using GestorActividades.Mapeos;
using GestorActividades.Servicios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioProyecto, RepositorioProyecto>();
builder.Services.AddScoped<IRepositorioActividad, RepositorioActividad>();

// Servicios
builder.Services.AddScoped<IServicioActividad, ServicioActividad>();
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IServicioProyecto, ServicioProyecto>();


// Middleware y utilidades
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddAutoMapper(typeof(MapeoActividadProfile));

var app = builder.Build();

app.UseMiddleware<ManejadorExcepcionesMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
