namespace GestorActividades.DTOs.Proyecto
{
    public class ProyectoDto
    {
        public Guid ProyectoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
