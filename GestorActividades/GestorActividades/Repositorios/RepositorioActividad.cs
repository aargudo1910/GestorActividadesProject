using GestorActividades.Datos;
using GestorActividades.Entidades;
using GestorActividades.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Repositorios
{
    public class RepositorioActividad: RepositorioGenerico<Actividad>, IRepositorioActividad
    {
        private readonly AppDbContext _contexto;

        public RepositorioActividad(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Actividad>> ObtenerPorProyectoAsync(Guid proyectoId)
        {
            return await _contexto.Actividades
                .Where(a => a.ProyectoId == proyectoId && a.Estado != "Eliminado")
                .ToListAsync();
        }

        public async Task<IEnumerable<Actividad>> ObtenerPorUsuarioYRangoFechasAsync(Guid usuarioId, DateTime desde, DateTime hasta)
        {
            return await _contexto.Actividades
                .Include(a => a.Proyecto)
                .Where(a =>
                    a.Proyecto.UsuarioId == usuarioId &&
                    a.Fecha >= desde &&
                    a.Fecha <= hasta &&
                    a.Estado != "Eliminado")
                .ToListAsync();
        }
    }
}
