using GestorActividades.DTOs.Actividad;

namespace GestorActividades.Servicios
{
    public interface IServicioActividad
    {
        Task<IEnumerable<ActividadDto>> ObtenerTodosAsync();
        Task<ActividadDto> ObtenerPorIdAsync(Guid id);
        Task<ActividadDto> CrearAsync(ActividadDto dto);
        Task<bool> ActualizarAsync(Guid id, ActividadDto dto);
        Task<bool> EliminarAsync(Guid id);
    }
}
