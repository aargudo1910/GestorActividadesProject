using GestorActividades.Excepciones.Base;

namespace GestorActividades.Excepciones.Usuarios
{
    public class ExcepcionUsuarioNoEncontrado : ExcepcionNoEncontrado
    {
        public ExcepcionUsuarioNoEncontrado() : base("El usuario no existe o fue eliminado.") { }
    }
}
