using System;

namespace GestorActividades.Excepciones
{
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException()
            : base("El usuario asociado no existe o está eliminado.")
        {
        }

        public UsuarioNoEncontradoException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
