using GestorActividades.DTOs.Usuario;

namespace GestorActividades.Servicios.UsuarioServicio
{
    public interface IServicioUsuario
    {
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync();
        Task<UsuarioDto> ObtenerPorIdAsync(Guid id);
        Task<UsuarioDto> CrearAsync(UsuarioCreateDto dto);
        Task<bool> ActualizarAsync(Guid id, UsuarioUpdateDto dto);
        Task<bool> EliminarAsync(Guid id);
    }
}
