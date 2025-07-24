using System;
using System.Collections.Generic;

namespace GestorActividades.Entidades;

public partial class Actividad : EntidadBase
{
    public Guid ActividadId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    public decimal HorasEstimadas { get; set; }

    public decimal HorasReales { get; set; }

    public Guid ProyectoId { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
