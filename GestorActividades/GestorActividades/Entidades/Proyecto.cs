using System;
using System.Collections.Generic;

namespace GestorActividades.Entidades;

public partial class Proyecto : EntidadBase
{
    public Guid ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public Guid UsuarioId { get; set; }

    public virtual ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();

    public virtual Usuario Usuario { get; set; } = null!;
}
