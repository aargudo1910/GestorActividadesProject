namespace GestorActividades.DTOs.Reportes
{
    public class ProyectoReporteDto
    {
        public string Nombre { get; set; }
        public List<ActividadReporteDto> Actividades { get; set; }
    }
}
