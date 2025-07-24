using GestorActividades.DTOs.Usuario;
using GestorActividades.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorActividades.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuario;

        public UsuarioController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        // GET: /api/usuarios
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarios = await _servicioUsuario.ObtenerTodosAsync();
            return Ok(usuarios);
        }

        // GET: /api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var usuario = await _servicioUsuario.ObtenerPorIdAsync(id);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        // POST: /api/usuarios
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoUsuario = await _servicioUsuario.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.UsuarioId }, nuevoUsuario);
        }

        // PUT: /api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] UsuarioUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _servicioUsuario.ActualizarAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent(); // 204
        }

        // DELETE: /api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _servicioUsuario.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent(); // 204
        }
    }
}
