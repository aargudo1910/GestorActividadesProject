using GestorActividades.DTOs.Actividade;
using GestorActividades.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorActividades.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadController : ControllerBase
    {
        private readonly IServicioActividad _servicioActividad;

        public ActividadController(IServicioActividad servicioActividad)
        {
            _servicioActividad = servicioActividad;
        }

        // GET: /api/actividades
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var actividades = await _servicioActividad.ObtenerTodosAsync();
            return Ok(actividades);
        }

        // GET: /api/actividades/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var actividad = await _servicioActividad.ObtenerPorIdAsync(id);
            if (actividad == null)
                return NotFound();

            return Ok(actividad);
        }

        // POST: /api/actividades
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ActividadDto dto)
        {
            var resultado = await _servicioActividad.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.ActividadId }, resultado);
        }

        // PUT: /api/actividades/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActividadDto dto)
        {
            var actualizado = await _servicioActividad.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent(); // 204
        }

        // DELETE: /api/actividades/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _servicioActividad.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent(); // 204
        }
    }
}
