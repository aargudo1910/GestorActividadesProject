using System.ComponentModel.DataAnnotations;

namespace GestorActividades.DTOs.Usuario
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe superar los 100 caracteres.")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no debe superar los 100  caracteres.")]
        public string Correo { get; set; }

        [StringLength(10, ErrorMessage = "El teléfono debe tener como máximo 10 caracteres.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El teléfono solo debe contener números.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("Admin|Editor|Viewer", ErrorMessage = "El rol debe ser Admin, Editor o Viewer.")]
        public string Rol { get; set; }
    }
}
