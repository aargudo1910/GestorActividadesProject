using GestorActividades.Entidades;

namespace GestorActividades.Repositorios.ProyectoRepositorio
{
    public interface IRepositorioProyecto : IRepositorioGenerico<Proyecto>
    {
        Task<IEnumerable<Proyecto>> ObtenerTodosActivosAsync();
        Task<Proyecto?> ObtenerPorIdActivoAsync(Guid id);
    
        Task<IEnumerable<Proyecto>> ObtenerPorUsuarioAsync(Guid usuarioId);
        Task<bool> EstaActivoAsync(Guid proyectoId);
        Task<Proyecto?> ObtenerConActividadesAsync(Guid proyectoId);
    }
}
