namespace GestorActividades.DTOs.Reporte
{
    public class ReporteUsuarioDto
    {
        public string Usuario { get; set; }
        public List<ProyectoReporteDto> Proyectos { get; set; } = new();
    }
}
