using GestorActividades.DTOs.Proyecto;
using GestorActividades.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorActividades.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly IServicioProyecto _servicioProyecto;
        private readonly ILogger<ProyectosController> _logger;

        public ProyectosController(IServicioProyecto servicioProyecto, ILogger<ProyectosController> logger)
        {
            _servicioProyecto = servicioProyecto;
            _logger = logger;
        }

        // GET: /api/proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectoDto>>> ObtenerTodos()
        {
            var proyectos = await _servicioProyecto.ObtenerTodosAsync();
            return Ok(proyectos);
        }

        // GET: /api/proyectos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDto>> ObtenerPorId(Guid id)
        {
            var proyecto = await _servicioProyecto.ObtenerPorIdAsync(id);
            if (proyecto == null)
                return NotFound();

            return Ok(proyecto);
        }

        // POST: /api/proyectos
        [HttpPost]
        public async Task<ActionResult<ProyectoDto>> Crear([FromBody] ProyectoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = await _servicioProyecto.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.ProyectoId }, creado);
        }

        // PUT: /api/proyectos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarProyectoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _servicioProyecto.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: /api/proyectos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _servicioProyecto.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
