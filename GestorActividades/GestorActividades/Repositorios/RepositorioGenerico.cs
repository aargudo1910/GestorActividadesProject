using GestorActividades.Datos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestorActividades.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        protected readonly AppDbContext _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(AppDbContext contexto)
        {
            _contexto = contexto;
            _dbSet = contexto.Set<T>();
        }

        public async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> condicion) =>
            await _dbSet.Where(condicion).ToListAsync();

        public async Task AgregarAsync(T entidad) => await _dbSet.AddAsync(entidad);

        public void Actualizar(T entidad) => _dbSet.Update(entidad);

        public async Task GuardarCambiosAsync() => await _contexto.SaveChangesAsync();
    }
}
