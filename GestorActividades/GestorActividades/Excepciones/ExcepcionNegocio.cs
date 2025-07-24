using System;

namespace GestorActividades.Excepciones
{
    public class ExcepcionNegocio : Exception
    {
        public ExcepcionNegocio(string mensaje) : base(mensaje) { }
    }
}
