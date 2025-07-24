using System.Linq.Expressions;

namespace GestorActividades.Repositorios.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> condicion);
        Task AgregarAsync(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        Task GuardarCambiosAsync();
    }
}
