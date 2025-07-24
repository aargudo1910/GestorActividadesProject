using GestorActividades.Datos;
using GestorActividades.Entidades;
using GestorActividades.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Repositorios
{
    public class RepositorioUsuario: RepositorioGenerico<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(AppDbContext context) : base(context) { }

        public async Task<bool> CorreoExiste(string correo)
        {
            return await _dbSet.AnyAsync(u => u.Correo == correo);
        }
    }
}
