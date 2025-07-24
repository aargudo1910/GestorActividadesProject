using System.ComponentModel.DataAnnotations;

namespace GestorActividades.DTOs.Actividade
{
    public class ActualizarActividadDto
    {

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no debe superar los 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de la actividad es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Las horas estimadas son obligatoria.")]
        [Range(1, 100, ErrorMessage = "Las horas estimadas deben ser un valor positivo mayor a cero.")]
        public decimal HorasEstimadas { get; set; }

        [Required(ErrorMessage = "Las horas reales son obligatorias.")]
        [Range(1, 100, ErrorMessage = "Las horas reales deben ser un valor positivo mayor a cero.")]
        public decimal HorasReales { get; set; }

        [Required(ErrorMessage = "El identificador del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

        [Required(ErrorMessage = "El estado del proyecto es obligatorio.")]
        [RegularExpression("Activo|Inactivo", ErrorMessage = "El estado debe ser Activo, Inactivo.")] 
        public string Estado { get; set; }

    }
}
