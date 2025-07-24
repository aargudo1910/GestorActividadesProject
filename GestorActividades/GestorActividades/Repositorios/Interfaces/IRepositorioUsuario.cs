using GestorActividades.Entidades;
namespace GestorActividades.Repositorios.Interfaces
{
    public interface IRepositorioUsuario : IRepositorioGenerico<Usuario>
    {
        Task<IEnumerable<Usuario>> ObtenerTodosActivosAsync();
        Task<Usuario?> ObtenerPorIdActivoAsync(Guid id);

        Task<Usuario?> BuscarPorCorreoAsync(string correo);
    }
}
