using GestorActividades.Excepciones.Base;

namespace GestorActividades.Excepciones.Usuarios
{
    public class ExcepcionCorreoDuplicado : ExcepcionConflicto
    {
        public ExcepcionCorreoDuplicado() : base("El correo ya está registrado.") { }
    }
}
