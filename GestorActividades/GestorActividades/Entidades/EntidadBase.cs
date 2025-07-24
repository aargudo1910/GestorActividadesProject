namespace GestorActividades.Entidades
{
    public abstract class EntidadBase
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Estado { get; set; }
    }
}
