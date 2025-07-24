using GestorActividades.DTOs.Actividade;
using GestorActividades.Servicios.ActividadServicio;
using Microsoft.AspNetCore.Mvc;

namespace GestorActividades.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadesController : ControllerBase
    {
        private readonly IServicioActividad _servicio;

        public ActividadesController(IServicioActividad servicio)
        {
            _servicio = servicio;
        }

        // GET: /api/actividades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActividadDto>>> ObtenerTodos()
        {
            var actividades = await _servicio.ObtenerTodosAsync();
            return Ok(actividades);
        }

        // GET: /api/actividades/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActividadDto>> ObtenerPorId(Guid id)
        {
            var actividad = await _servicio.ObtenerPorIdAsync(id);
            if (actividad is null)
                return NotFound();

            return Ok(actividad);
        }

        // POST: /api/actividades
        [HttpPost]
        public async Task<ActionResult<ActividadDto>> Crear([FromBody] CrearActividadDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creada = await _servicio.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.ActividadId }, creada);
        }

        // PUT: /api/actividades/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarActividadDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _servicio.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: /api/actividades/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _servicio.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
