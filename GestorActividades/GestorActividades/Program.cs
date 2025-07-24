using GestorActividades.Datos;
using GestorActividades.Repositorios.Interfaces;
using GestorActividades.Repositorios;
using Microsoft.EntityFrameworkCore;
using GestorActividades.Middleware;
using GestorActividades.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioProyecto, RepositorioProyecto>();
builder.Services.AddScoped<IRepositorioActividad, RepositorioActividad>();

// Servicios
builder.Services.AddScoped<IServicioActividad, ServicioActividad>();


// Middleware y utilidades
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
