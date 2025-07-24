using GestorActividades.Datos;
using GestorActividades.Entidades;
using GestorActividades.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Repositorios
{
    public class RepositorioProyecto : RepositorioGenerico<Proyecto>, IRepositorioProyecto
    {
        private readonly AppDbContext _contexto;

        public RepositorioProyecto(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Proyecto>> ObtenerPorUsuarioAsync(Guid usuarioId)
        {
            return await _contexto.Proyectos
                .Where(p => p.UsuarioId == usuarioId && p.Estado != "Eliminado")
                .ToListAsync();
        }

        public async Task<bool> EstaActivoAsync(Guid proyectoId)
        {
            var proyecto = await _contexto.Proyectos
                .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId);

            return proyecto is not null && proyecto.Estado == "Activo";
        }

        public async Task<Proyecto?> ObtenerConActividadesAsync(Guid proyectoId)
        {
            return await _contexto.Proyectos
                .Include(p => p.Actividades)
                .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId);
        }
    }
}
