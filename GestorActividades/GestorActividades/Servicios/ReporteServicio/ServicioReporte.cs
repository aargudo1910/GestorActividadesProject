using GestorActividades.DTOs.Reporte;
using GestorActividades.Excepciones.Proyectos;
using GestorActividades.Excepciones.Usuarios;
using GestorActividades.Repositorios.ActividadRepositorio;
using GestorActividades.Repositorios.UsuarioRepositorio;
namespace GestorActividades.Servicios.ReporteServicio
{
    public class ServicioReporte : IServicioReporte
    {
        private readonly IRepositorioActividad _repoActividad;
        private readonly IRepositorioUsuario _repoUsuario;

        public ServicioReporte(IRepositorioActividad repoActividad, IRepositorioUsuario repoUsuario)
        {
            _repoActividad = repoActividad;
            _repoUsuario = repoUsuario;
        }

        public async Task<ReporteUsuarioDto?> GenerarReporteAsync(Guid usuarioId, DateTime desde, DateTime hasta)
        {
            if (desde > hasta)
                throw new ExcepcionFechasInvalidas();

            var usuario = await _repoUsuario.ObtenerPorIdActivoAsync(usuarioId);
            if (usuario is null)
                throw new ExcepcionUsuarioNoEncontrado();


            var actividades = await _repoActividad.ObtenerPorUsuarioYRangoFechasAsync(usuarioId, desde, hasta);

            var agrupado = actividades
                .GroupBy(a => a.Proyecto.Nombre)
                .Select(g => new ProyectoReporteDto
                {
                    Nombre = g.Key,
                    Actividades = g.Select(a => new ActividadReporteDto
                    {
                        Fecha = a.Fecha,
                        Titulo = a.Titulo,
                        HorasReales = a.HorasReales
                    }).ToList()
                }).ToList();

            return new ReporteUsuarioDto
            {
                Usuario = usuario.NombreCompleto,
                Proyectos = agrupado
            };
        }
    }
}
