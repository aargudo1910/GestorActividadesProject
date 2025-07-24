using GestorActividades.Entidades;

namespace GestorActividades.Repositorios.ActividadRepositorio
{
    public interface IRepositorioActividad : IRepositorioGenerico<Actividade>
    {
        Task<IEnumerable<Actividade>> ObtenerTodosActivosAsync();
        Task<Actividade?> ObtenerPorIdActivoAsync(Guid id);
        Task<IEnumerable<Actividade>> ObtenerPorUsuarioYRangoFechasAsync(Guid usuarioId, DateTime desde, DateTime hasta);
    }
}
