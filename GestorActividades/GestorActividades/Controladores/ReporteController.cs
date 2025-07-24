using GestorActividades.Servicios.ReporteServicio;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reportes")]
public class ReporteController : ControllerBase
{
    private readonly IServicioReporte _servicioReporte;

    public ReporteController(IServicioReporte servicioReporte)
    {
        _servicioReporte = servicioReporte;
    }

    [HttpGet("actividades")]
    public async Task<IActionResult> ObtenerReporte(
        [FromQuery] Guid usuarioId,
        [FromQuery] DateTime desde,
        [FromQuery] DateTime hasta)
    {
        var resultado = await _servicioReporte.GenerarReporteAsync(usuarioId, desde, hasta);

        return Ok(resultado);
    }
}
