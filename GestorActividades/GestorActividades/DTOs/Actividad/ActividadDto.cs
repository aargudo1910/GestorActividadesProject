namespace GestorActividades.DTOs.Actividad
{
    public class ActividadDto
    {
        public Guid ActividadId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal HorasEstimadas { get; set; }
        public decimal HorasReales { get; set; }
        public Guid ProyectoId { get; set; }
    }
}
