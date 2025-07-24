using System;
using System.Collections.Generic;

namespace GestorActividades.Entidades;

public partial class Actividade : EntidadBase
{
    public Guid ActividadId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal HorasEstimadas { get; set; }

    public decimal? HorasReales { get; set; }

    public Guid ProyectoId { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
