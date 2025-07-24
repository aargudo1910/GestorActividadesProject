using GestorActividades.Excepciones;

public class ExcepcionCorreoDuplicado : ExcepcionNegocio
{
    public ExcepcionCorreoDuplicado() : base("Ya existe un usuario registrado con este correo.") { }
}
