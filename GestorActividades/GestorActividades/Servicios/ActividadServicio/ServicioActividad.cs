using GestorActividades.DTOs.Actividade;
using GestorActividades.Entidades;
using GestorActividades.Repositorios.ActividadRepositorio;
using GestorActividades.Repositorios.ProyectoRepositorio;

namespace GestorActividades.Servicios.ActividadServicio
{
    public class ServicioActividad : IServicioActividad
    {
        private readonly IRepositorioActividad _repositorioActividad;
        private readonly IRepositorioProyecto _repositorioProyecto;
        private readonly ILogger<ServicioActividad> _logger;

        public ServicioActividad(
            IRepositorioActividad repositorioActividad,
            IRepositorioProyecto repositorioProyecto,
            ILogger<ServicioActividad> logger)
        {
            _repositorioActividad = repositorioActividad;
            _repositorioProyecto = repositorioProyecto;
            _logger = logger;
        }

        public async Task<IEnumerable<ActividadDto>> ObtenerTodosAsync()
        {
            var actividades = await _repositorioActividad.ObtenerTodosActivosAsync();
            return actividades.Select(a => new ActividadDto
            {
                ActividadId = a.ActividadId,
                Titulo = a.Titulo,
                Descripcion = a.Descripcion,
                Fecha = a.Fecha.ToDateTime(TimeOnly.MinValue),
                HorasEstimadas = a.HorasEstimadas,
                HorasReales = a.HorasReales ?? 0,
                ProyectoId = a.ProyectoId,
                Estado = a.Estado
            });
        }

        public async Task<ActividadDto?> ObtenerPorIdAsync(Guid id)
        {
            var actividad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);
            if (actividad is null) return null;

            return new ActividadDto
            {
                ActividadId = actividad.ActividadId,
                Titulo = actividad.Titulo,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha.ToDateTime(TimeOnly.MinValue),
                HorasEstimadas = actividad.HorasEstimadas,
                HorasReales = actividad.HorasReales ?? 0,
                ProyectoId = actividad.ProyectoId,
                Estado = actividad.Estado
            };
        }

        public async Task<ActividadDto> CrearAsync(CrearActividadDto dto)
        {
            var proyecto = await _repositorioProyecto.ObtenerPorIdActivoAsync(dto.ProyectoId);

            if (proyecto is null || proyecto.Estado != "Activo")
                throw new ExcepcionActividadInvalida();

            var fecha = DateOnly.FromDateTime(dto.Fecha);
            if (fecha < proyecto.FechaInicio || fecha > proyecto.FechaFin)
                throw new ExcepcionActividadInvalida();

            var entidad = new Actividade
            {
                ActividadId = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Fecha = fecha,
                HorasEstimadas = dto.HorasEstimadas,
                HorasReales = dto.HorasReales,
                ProyectoId = dto.ProyectoId,
                Estado = "Activo",
                FechaCreacion = DateTime.UtcNow
            };

            await _repositorioActividad.AgregarAsync(entidad);
            await _repositorioActividad.GuardarCambiosAsync();


            return new ActividadDto
            {
                ActividadId = entidad.ActividadId,
                Titulo = entidad.Titulo,
                Descripcion = entidad.Descripcion,
                Fecha = dto.Fecha,
                HorasEstimadas = entidad.HorasEstimadas,
                HorasReales = entidad.HorasReales ?? 0,
                ProyectoId = entidad.ProyectoId
            };
        }

        public async Task<bool> ActualizarAsync(Guid id, ActualizarActividadDto dto)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            entidad.Titulo = dto.Titulo;
            entidad.Descripcion = dto.Descripcion;
            entidad.Fecha = DateOnly.FromDateTime(dto.Fecha);
            entidad.HorasEstimadas = dto.HorasEstimadas;
            entidad.HorasReales = dto.HorasReales;
            entidad.ProyectoId = dto.ProyectoId;
            entidad.Estado = dto.Estado;
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioActividad.Actualizar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);
            if (entidad is null) return false;

            entidad.Estado = "Eliminado";
            entidad.FechaModificacion = DateTime.UtcNow;

            _repositorioActividad.Actualizar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();

            return true;
        }
    }
}
