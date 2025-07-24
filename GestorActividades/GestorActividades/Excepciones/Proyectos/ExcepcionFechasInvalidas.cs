using GestorActividades.Excepciones.Base;

namespace GestorActividades.Excepciones.Proyectos
{
    public class ExcepcionFechasInvalidas : ExcepcionValidacion
    {
        public ExcepcionFechasInvalidas() : base("La fecha de fin no puede ser menor que la fecha de inicio.") { }
    }
}
