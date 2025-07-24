using GestorActividades.Datos;
using GestorActividades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Repositorios.UsuarioRepositorio
{
    public class RepositorioUsuario : RepositorioGenerico<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Usuario>> ObtenerTodosActivosAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(u => !u.Estado.Equals("Eliminado"))
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorIdActivoAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.UsuarioId == id &&
                    !u.Estado.Equals("Eliminado"));
        }

        public async Task<Usuario?> BuscarPorCorreoAsync(string correo)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u =>
                    u.Correo == correo &&
                    !u.Estado.Equals("Eliminado"));
        }
    }
}
