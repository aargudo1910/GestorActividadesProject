using GestorActividades.DTOs.Actividade;
using GestorActividades.Entidades;
using GestorActividades.Repositorios.ActividadRepositorio;
using GestorActividades.Repositorios.ProyectoRepositorio;
using GestorActividades.Servicios.ActividadServicio;
using Microsoft.Extensions.Logging;
using Moq;

public class ServicioActividadTests
{
    private readonly Mock<IRepositorioActividad> _mockRepoActividad;
    private readonly Mock<IRepositorioProyecto> _mockRepoProyecto;
    private readonly Mock<ILogger<ServicioActividad>> _mockLogger;
    private readonly ServicioActividad _servicio;

    public ServicioActividadTests()
    {
        _mockRepoActividad = new Mock<IRepositorioActividad>();
        _mockRepoProyecto = new Mock<IRepositorioProyecto>();
        _mockLogger = new Mock<ILogger<ServicioActividad>>();

        _servicio = new ServicioActividad(
            _mockRepoActividad.Object,
            _mockRepoProyecto.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task CrearAsync_DeberiaCrearActividad_CuandoProyectoActivoYFechaValida()
    {
        
        var proyectoId = Guid.NewGuid();
        var dto = new CrearActividadDto
        {
            Titulo = "Tarea 1",
            Descripcion = "Descripción de tarea",
            Fecha = new DateTime(2025, 07, 20),
            HorasEstimadas = 3,
            HorasReales = 2,
            ProyectoId = proyectoId
        };

        var proyecto = new Proyecto
        {
            ProyectoId = proyectoId,
            Estado = "Activo",
            FechaInicio = new DateOnly(2025, 07, 15),
            FechaFin = new DateOnly(2025, 07, 30)
        };

        _mockRepoProyecto.Setup(r => r.ObtenerPorIdActivoAsync(proyectoId)).ReturnsAsync(proyecto);
        _mockRepoActividad.Setup(r => r.AgregarAsync(It.IsAny<Actividade>())).Returns(Task.CompletedTask);
        _mockRepoActividad.Setup(r => r.GuardarCambiosAsync()).Returns(Task.CompletedTask);

      
        var resultado = await _servicio.CrearAsync(dto);

    
        Assert.NotNull(resultado);
        Assert.Equal(dto.Titulo, resultado.Titulo);
        Assert.Equal(dto.ProyectoId, resultado.ProyectoId);
    }
    [Fact]
    public async Task CrearAsync_DeberiaLanzarExcepcion_CuandoProyectoNoEsActivo()
    {
        
        var proyectoId = Guid.NewGuid();
        var dto = new CrearActividadDto
        {
            Titulo = "Tarea 2",
            Fecha = new DateTime(2025, 07, 20),
            ProyectoId = proyectoId
        };

        _mockRepoProyecto.Setup(r => r.ObtenerPorIdActivoAsync(proyectoId))
            .ReturnsAsync(new Proyecto
            {
                ProyectoId = proyectoId,
                Estado = "Inactivo"
            });

       
        await Assert.ThrowsAsync<ExcepcionActividadInvalida>(() => _servicio.CrearAsync(dto));
    }
    [Fact]
    public async Task CrearAsync_DeberiaLanzarExcepcion_CuandoFechaActividadFueraDeRango()
    {
        
        var proyectoId = Guid.NewGuid();
        var dto = new CrearActividadDto
        {
            Titulo = "Fuera de fecha",
            Fecha = new DateTime(2025, 08, 01),
            ProyectoId = proyectoId
        };

        var proyecto = new Proyecto
        {
            ProyectoId = proyectoId,
            Estado = "Activo",
            FechaInicio = new DateOnly(2025, 07, 01),
            FechaFin = new DateOnly(2025, 07, 31)
        };

        _mockRepoProyecto.Setup(r => r.ObtenerPorIdActivoAsync(proyectoId)).ReturnsAsync(proyecto);

       
        await Assert.ThrowsAsync<ExcepcionActividadInvalida>(() => _servicio.CrearAsync(dto));
    }

}
