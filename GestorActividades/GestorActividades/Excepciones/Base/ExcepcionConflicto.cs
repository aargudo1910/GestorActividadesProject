namespace GestorActividades.Excepciones.Base
{
    public class ExcepcionConflicto : ExcepcionNegocio
    {
        public ExcepcionConflicto(string mensaje) : base(mensaje) { }
    }
}
