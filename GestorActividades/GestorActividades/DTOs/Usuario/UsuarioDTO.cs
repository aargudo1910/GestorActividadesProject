using System;
using System.ComponentModel.DataAnnotations;

namespace GestorActividades.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public Guid UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
    }
}
