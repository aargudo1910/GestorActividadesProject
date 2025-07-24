using System;
using System.Collections.Generic;

namespace GestorActividades.Entidades;

public partial class Usuario : EntidadBase
{
public Guid UsuarioId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
