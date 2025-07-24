using GestorActividades.DTOs.Actividad;
using GestorActividades.Entidades;
using GestorActividades.Excepciones;
using GestorActividades.Repositorios.Interfaces;

namespace GestorActividades.Servicios
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
            var actividades = await _repositorioActividad.ObtenerTodosAsync();

            return actividades.Select(a => new ActividadDto
            {
                ActividadId = a.ActividadId,
                Titulo = a.Titulo,
                Descripcion = a.Descripcion,
                Fecha = a.Fecha,
                HorasEstimadas = a.HorasEstimadas,
                HorasReales = a.HorasReales,
                ProyectoId = a.ProyectoId
            });
        }

        public async Task<ActividadDto> ObtenerPorIdAsync(Guid id)
        {
            var actividad = await _repositorioActividad.ObtenerPorIdAsync(id);

            if (actividad == null) return null;

            return new ActividadDto
            {
                ActividadId = actividad.ActividadId,
                Titulo = actividad.Titulo,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                HorasEstimadas = actividad.HorasEstimadas,
                HorasReales = actividad.HorasReales,
                ProyectoId = actividad.ProyectoId
            };
        }

        public async Task<ActividadDto> CrearAsync(ActividadDto dto)
        {
            var proyecto = await _repositorioProyecto.ObtenerPorIdAsync(dto.ProyectoId);

            var fueraDeRango = dto.Fecha < proyecto.FechaInicio || dto.Fecha > proyecto.FechaFin;
            var proyectoInactivo = proyecto == null || proyecto.Estado != "Activo";

            if (proyectoInactivo || fueraDeRango)
                throw new ExcepcionActividadInvalida();

            var entidad = new Actividad
            {
                ActividadId = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                HorasEstimadas = dto.HorasEstimadas,
                HorasReales = dto.HorasReales,
                ProyectoId = dto.ProyectoId
            };

            await _repositorioActividad.AgregarAsync(entidad);
            await _repositorioActividad.GuardarCambiosAsync();
            dto.ActividadId = entidad.ActividadId;
            return dto;
        }

        public async Task<bool> ActualizarAsync(Guid id, ActividadDto dto)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdAsync(id);
            if (entidad == null) return false;

            entidad.Titulo = dto.Titulo;
            entidad.Descripcion = dto.Descripcion;
            entidad.Fecha = dto.Fecha;
            entidad.HorasEstimadas = dto.HorasEstimadas;
            entidad.HorasReales = dto.HorasReales;
            entidad.ProyectoId = dto.ProyectoId;
            _repositorioActividad.Actualizar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdAsync(id);
            if (entidad == null) return false;

            _repositorioActividad.Eliminar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();
            return true;
        }
    }
}
