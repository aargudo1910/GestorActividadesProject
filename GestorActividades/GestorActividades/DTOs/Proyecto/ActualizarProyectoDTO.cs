using System.ComponentModel.DataAnnotations;

namespace GestorActividades.DTOs.Proyecto
{
    public class ProyectoUpdateDto
    {
        [Required(ErrorMessage = "El identificador del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El estado del proyecto es obligatorio.")]
        [RegularExpression("Activo|Inactivo|Eliminado", ErrorMessage = "El estado debe ser Activo, Inactivo o Eliminado.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public Guid UsuarioId { get; set; }
    }
}
namespace GestorActividades.DTOs.Proyecto
{
    public class ActualizarProyectoDTO
    {
    }
}
