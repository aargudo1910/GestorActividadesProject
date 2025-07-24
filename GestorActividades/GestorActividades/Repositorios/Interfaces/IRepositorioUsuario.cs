using GestorActividades.Entidades;
namespace GestorActividades.Repositorios.Interfaces
{
    public interface IRepositorioUsuario : IRepositorioGenerico<Usuario>
    {
        Task<bool> CorreoExiste(string correo);
    }
}
