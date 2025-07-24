using GestorActividades.DTOs.Actividade;

namespace GestorActividades.Servicios.ActividadServicio
{
    public interface IServicioActividad
    {
        Task<IEnumerable<ActividadDto>> ObtenerTodosAsync();
        Task<ActividadDto?> ObtenerPorIdAsync(Guid id);
        Task<ActividadDto> CrearAsync(CrearActividadDto dto);
        Task<bool> ActualizarAsync(Guid id, ActualizarActividadDto dto);
        Task<bool> EliminarAsync(Guid id);
    }
}
