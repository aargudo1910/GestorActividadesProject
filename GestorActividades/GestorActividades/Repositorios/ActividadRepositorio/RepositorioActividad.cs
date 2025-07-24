using GestorActividades.Datos;
using GestorActividades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Repositorios.ActividadRepositorio
{
    public class RepositorioActividad : RepositorioGenerico<Actividade>, IRepositorioActividad
    {
        public RepositorioActividad(AppDbContext contexto) : base(contexto) { }

        public async Task<IEnumerable<Actividade>> ObtenerTodosActivosAsync()
        {
            return await _dbSet
                .Include(a => a.Proyecto)
                .AsNoTracking()
                .Where(a => !a.Estado.Equals("Eliminado"))
                .ToListAsync();
        }

        public async Task<Actividade?> ObtenerPorIdActivoAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Proyecto)
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.ActividadId == id &&
                    !a.Estado.Equals("Eliminado"));
        }

        public async Task<IEnumerable<Actividade>> ObtenerPorUsuarioYRangoFechasAsync(Guid usuarioId, DateTime desde, DateTime hasta)
        {
            var desdeDateOnly = DateOnly.FromDateTime(desde);
            var hastaDateOnly = DateOnly.FromDateTime(hasta);

            return await _dbSet
                .Include(a => a.Proyecto)
                .Where(a => a.Proyecto.UsuarioId == usuarioId
                            && a.Fecha >= desdeDateOnly
                            && a.Fecha <= hastaDateOnly
                            && a.Estado != "Eliminado")
                .ToListAsync();
        }
    }
}
