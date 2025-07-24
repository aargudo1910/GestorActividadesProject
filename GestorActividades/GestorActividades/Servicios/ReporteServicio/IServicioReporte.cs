using GestorActividades.DTOs.Reporte;
namespace GestorActividades.Servicios.ReporteServicio
{
    public interface IServicioReporte
    {
        Task<ReporteUsuarioDto?> GenerarReporteAsync(Guid usuarioId, DateTime desde, DateTime hasta);
    }
}


