using GestorActividades.DTOs.Proyecto;
using GestorActividades.Entidades;
using GestorActividades.Excepciones;
using GestorActividades.Repositorios.Interfaces;
using GestorActividades.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace GestorActividades.Servicios
{
    public class ServicioProyecto : IServicioProyecto
    {
        private readonly IRepositorioProyecto _repositorioProyecto;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly ILogger<ServicioProyecto> _logger;

        public ServicioProyecto(
            IRepositorioProyecto repositorioProyecto,
            IRepositorioUsuario repositorioUsuario,
            ILogger<ServicioProyecto> logger)
        {
            _repositorioProyecto = repositorioProyecto;
            _repositorioUsuario = repositorioUsuario;
            _logger = logger;
        }

        public async Task<IEnumerable<ProyectoDto>> ObtenerTodosAsync()
        {
            var proyectos = await _repositorioProyecto.ObtenerTodosActivosAsync();
            return proyectos.Select(p => new ProyectoDto
            {
                ProyectoId = p.ProyectoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaInicio = p.FechaInicio.ToDateTime(TimeOnly.MinValue),
                FechaFin = p.FechaFin.ToDateTime(TimeOnly.MinValue),
                Estado = p.Estado,
                UsuarioId = p.UsuarioId
            });
        }

        public async Task<ProyectoDto?> ObtenerPorIdAsync(Guid id)
        {
            var proyecto = await _repositorioProyecto.ObtenerPorIdActivoAsync(id);
            if (proyecto == null) return null;

            return new ProyectoDto
            {
                ProyectoId = proyecto.ProyectoId,
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                FechaInicio = proyecto.FechaInicio.ToDateTime(TimeOnly.MinValue),
                FechaFin = proyecto.FechaFin.ToDateTime(TimeOnly.MinValue),
                Estado = proyecto.Estado,
                UsuarioId = proyecto.UsuarioId
            };
        }

        public async Task<ProyectoDto> CrearAsync(ProyectoCreateDto dto)
        {
            var usuario = await _repositorioUsuario.ObtenerPorIdActivoAsync(dto.UsuarioId);
            if (usuario is null)
                throw new ExcepcionNegocio("El usuario asociado no existe o está eliminado.");

            var entidad = new Proyecto
            {
                ProyectoId = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaInicio = DateOnly.FromDateTime(dto.FechaInicio),
                FechaFin = DateOnly.FromDateTime(dto.FechaFin),
                Estado = "Activo",
                UsuarioId = dto.UsuarioId,
                FechaCreacion = DateTime.UtcNow
            };

            await _repositorioProyecto.AgregarAsync(entidad);
            await _repositorioProyecto.GuardarCambiosAsync();

            return new ProyectoDto
            {
                ProyectoId = entidad.ProyectoId,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = entidad.Estado,
                UsuarioId = entidad.UsuarioId
            };
        }

        public async Task<bool> ActualizarAsync(Guid id, ProyectoUpdateDto dto)
        {
            var entidad = await _repositorioProyecto.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            entidad.Nombre = dto.Nombre;
            entidad.Descripcion = dto.Descripcion;
            entidad.FechaInicio = DateOnly.FromDateTime(dto.FechaInicio);
            entidad.FechaFin = DateOnly.FromDateTime(dto.FechaFin);
            entidad.Estado = dto.Estado;
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioProyecto.Actualizar(entidad);
            await _repositorioProyecto.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var entidad = await _repositorioProyecto.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            entidad.Estado = "Eliminado";
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioProyecto.Actualizar(entidad);
            await _repositorioProyecto.GuardarCambiosAsync();

            return true;
        }
    }
}
