using System;

namespace GestorActividades.Excepciones
{
    public class FechasInvalidasException : Exception
    {
        public FechasInvalidasException()
            : base("La fecha de fin no puede ser menor a la fecha de inicio.")
        {
        }

        public FechasInvalidasException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
