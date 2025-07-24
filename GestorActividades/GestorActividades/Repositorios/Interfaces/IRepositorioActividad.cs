using GestorActividades.Entidades;

namespace GestorActividades.Repositorios.Interfaces
{
    public interface IRepositorioActividad : IRepositorioGenerico<Actividad>
    {
        Task<IEnumerable<Actividad>> ObtenerPorProyectoAsync(Guid proyectoId);
        Task<IEnumerable<Actividad>> ObtenerPorUsuarioYRangoFechasAsync(Guid usuarioId, DateTime desde, DateTime hasta);
    }
}
