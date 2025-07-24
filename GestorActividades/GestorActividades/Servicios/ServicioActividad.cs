using AutoMapper;
using GestorActividades.DTOs.Actividade;
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
			var actividades = await _repositorioActividad.ObtenerTodosActivosAsync();

			return actividades.Select(a => new ActividadDto
			{
				ActividadId = a.ActividadId,
				Titulo = a.Titulo,
				Descripcion = a.Descripcion,
				Fecha = a.Fecha.ToDateTime(TimeOnly.MinValue), 
				HorasEstimadas = a.HorasEstimadas,
				HorasReales = a.HorasReales ?? 0, 
				ProyectoId = a.ProyectoId
			});
		}


		public async Task<ActividadDto> ObtenerPorIdAsync(Guid id)
        {
            var actividad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);

            if (actividad == null) return null;

            return new ActividadDto
            {
                ActividadId = actividad.ActividadId,
                Titulo = actividad.Titulo,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha.ToDateTime(TimeOnly.MinValue),
                HorasEstimadas = actividad.HorasEstimadas,
                HorasReales = actividad.HorasReales ?? 0,
                ProyectoId = actividad.ProyectoId
            };
        }

		public async Task<ActividadDto> CrearAsync(ActividadDto dto)
		{
			var proyecto = await _repositorioProyecto.ObtenerPorIdActivoAsync(dto.ProyectoId);

			if (proyecto == null || proyecto.Estado != "Activo")
				throw new ExcepcionActividadInvalida();

			var fechaActividad = DateOnly.FromDateTime(dto.Fecha);

			var fueraDeRango = fechaActividad < proyecto.FechaInicio || fechaActividad > proyecto.FechaFin;
			if (fueraDeRango)
				throw new ExcepcionActividadInvalida();

			var entidad = new Actividade
			{
				ActividadId = Guid.NewGuid(),
				Titulo = dto.Titulo,
				Descripcion = dto.Descripcion,
				Fecha = fechaActividad, 
				HorasEstimadas = dto.HorasEstimadas,
				HorasReales = dto.HorasReales,
				ProyectoId = dto.ProyectoId,
				Estado = "Activo",
				FechaCreacion = DateTime.UtcNow
			};

			await _repositorioActividad.AgregarAsync(entidad);
			await _repositorioActividad.GuardarCambiosAsync();

			dto.ActividadId = entidad.ActividadId;
			return dto;
		}


		public async Task<bool> ActualizarAsync(Guid id, ActividadDto dto)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);
            if (entidad == null) return false;

			var fechaActividad = DateOnly.FromDateTime(dto.Fecha);

			entidad.Titulo = dto.Titulo;
            entidad.Descripcion = dto.Descripcion;
            entidad.Fecha = fechaActividad;
            entidad.HorasEstimadas = dto.HorasEstimadas;
            entidad.HorasReales = dto.HorasReales;
            entidad.ProyectoId = dto.ProyectoId;
            _repositorioActividad.Actualizar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var entidad = await _repositorioActividad.ObtenerPorIdActivoAsync(id);
            if (entidad == null) return false;

            _repositorioActividad.Actualizar(entidad);
            await _repositorioActividad.GuardarCambiosAsync();
            return true;
        }
    }
}
