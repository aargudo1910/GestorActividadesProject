namespace GestorActividades.DTOs.Reporte
{
    public class ActividadReporteDto
    {
        public DateOnly Fecha { get; set; }
        public string Titulo { get; set; }
        public decimal? HorasReales { get; set; }
    }
}
