using System;
using System.Collections.Generic;

namespace GestorActividades.Entidades;

public partial class Proyecto : EntidadBase
{
    public Guid ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public Guid UsuarioId { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();

    public virtual Usuario Usuario { get; set; } = null!;
}
