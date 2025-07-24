using System.ComponentModel.DataAnnotations;

namespace GestorActividades.DTOs.Proyecto
{
    public class ProyectoCreateDto
    {
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public Guid UsuarioId { get; set; }
    }
}
