using System.Linq.Expressions;

namespace GestorActividades.Repositorios
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> condicion);
        Task AgregarAsync(T entidad);
        void Actualizar(T entidad);
        Task GuardarCambiosAsync();
    }
}
