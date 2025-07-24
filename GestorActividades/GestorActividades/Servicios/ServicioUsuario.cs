using GestorActividades.DTOs.Usuario;
using GestorActividades.Entidades;
using GestorActividades.Excepciones.Usuarios;
using GestorActividades.Repositorios.Interfaces;
using Microsoft.Extensions.Logging;

namespace GestorActividades.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly ILogger<ServicioUsuario> _logger;

        public ServicioUsuario(
            IRepositorioUsuario repositorioUsuario,
            ILogger<ServicioUsuario> logger)
        {
            _repositorioUsuario = repositorioUsuario;
            _logger = logger;
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _repositorioUsuario.ObtenerTodosActivosAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                UsuarioId = u.UsuarioId,
                NombreCompleto = u.NombreCompleto,
                Correo = u.Correo,
                Telefono = u.Telefono,
                Rol = u.Rol,
                Estado = u.Estado
            });
        }

        public async Task<UsuarioDto> ObtenerPorIdAsync(Guid id)
        {
            var usuario = await _repositorioUsuario.ObtenerPorIdActivoAsync(id);
            if (usuario is null) return null;

            return new UsuarioDto
            {
                UsuarioId = usuario.UsuarioId,
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol,
                Estado = usuario.Estado
            };
        }

        public async Task<UsuarioDto> CrearAsync(UsuarioCreateDto dto)
        {
            var entidadCorreo = await _repositorioUsuario.BuscarPorCorreoAsync(dto.Correo);
            if (entidadCorreo != null)
                throw new ExcepcionCorreoDuplicado();

            var entidad = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                NombreCompleto = dto.NombreCompleto,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Rol = dto.Rol,
                Estado = "Activo"
            };

            await _repositorioUsuario.AgregarAsync(entidad);
            await _repositorioUsuario.GuardarCambiosAsync();

            return new UsuarioDto
            {
                UsuarioId = entidad.UsuarioId,
                NombreCompleto = entidad.NombreCompleto,
                Correo = entidad.Correo,
                Telefono = entidad.Telefono,
                Rol = entidad.Rol,
                Estado = entidad.Estado
            };
        }


        public async Task<bool> ActualizarAsync(Guid id, UsuarioUpdateDto dto)
        {
            var entidad = await _repositorioUsuario.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            var otroUsuario = await _repositorioUsuario.BuscarPorCorreoAsync(dto.Correo);
            if (otroUsuario != null && otroUsuario.UsuarioId != id)
                throw new ExcepcionCorreoDuplicado();

            entidad.NombreCompleto = dto.NombreCompleto;
            entidad.Correo = dto.Correo;
            entidad.Telefono = dto.Telefono;
            entidad.Rol = dto.Rol;
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioUsuario.Actualizar(entidad);
            await _repositorioUsuario.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var entidad = await _repositorioUsuario.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            entidad.Estado = "Eliminado";
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioUsuario.Actualizar(entidad);
            await _repositorioUsuario.GuardarCambiosAsync();

            return true;
        }
    }
}
