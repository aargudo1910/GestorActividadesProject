using GestorActividades.DTOs.Proyecto;

namespace GestorActividades.Servicios.ProyectoServicio
{
    public interface IServicioProyecto
    {
        Task<IEnumerable<ProyectoDto>> ObtenerTodosAsync();
        Task<ProyectoDto?> ObtenerPorIdAsync(Guid id);
        Task<ProyectoDto> CrearAsync(ProyectoCreateDto dto);
        Task<bool> ActualizarAsync(Guid id, ActualizarProyectoDto dto);
        Task<bool> EliminarAsync(Guid id);
    }
}
