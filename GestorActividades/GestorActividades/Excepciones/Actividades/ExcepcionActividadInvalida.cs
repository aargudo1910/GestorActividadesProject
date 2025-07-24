using GestorActividades.Excepciones.Base;

public class ExcepcionActividadInvalida : ExcepcionValidacion
{
    public ExcepcionActividadInvalida()
        : base("No se puede registrar actividad en un proyecto inactivo o fuera de fecha.") { }
}
