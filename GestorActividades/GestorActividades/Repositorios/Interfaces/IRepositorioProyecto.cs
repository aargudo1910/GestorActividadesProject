using GestorActividades.Entidades;

namespace GestorActividades.Repositorios.Interfaces
{
    public interface IRepositorioProyecto : IRepositorioGenerico<Proyecto>
    {
        Task<IEnumerable<Proyecto>> ObtenerPorUsuarioAsync(Guid usuarioId);
        Task<bool> EstaActivoAsync(Guid proyectoId);
        Task<Proyecto?> ObtenerConActividadesAsync(Guid proyectoId);
    }
}
