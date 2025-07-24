namespace GestorActividades.Excepciones
{
    public class ExcepcionActividadInvalida : ExcepcionNegocio
    {
        public ExcepcionActividadInvalida()
            : base("No se puede registrar actividad en un proyecto inactivo o fuera de fecha.") { }
    }
}
