using GestorActividades.DTOs.Proyecto;
using GestorActividades.Entidades;
using GestorActividades.Excepciones;
using GestorActividades.Excepciones.Usuarios;
using GestorActividades.Repositorios.ProyectoRepositorio;
using GestorActividades.Repositorios.UsuarioRepositorio;
using GestorActividades.Servicios.ProyectoServicio;
using Microsoft.Extensions.Logging;
using Moq;

public class ServicioProyectoTests
{
    private readonly Mock<IRepositorioProyecto> _mockRepoProyecto;
    private readonly Mock<IRepositorioUsuario> _mockRepoUsuario;
    private readonly Mock<ILogger<ServicioProyecto>> _mockLogger;
    private readonly ServicioProyecto _servicio;

    public ServicioProyectoTests()
    {
        _mockRepoProyecto = new Mock<IRepositorioProyecto>();
        _mockRepoUsuario = new Mock<IRepositorioUsuario>();
        _mockLogger = new Mock<ILogger<ServicioProyecto>>();

        _servicio = new ServicioProyecto(
            _mockRepoProyecto.Object,
            _mockRepoUsuario.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task CrearAsync_DeberiaCrearProyecto_CuandoUsuarioExiste()
    {
        
        var dto = new ProyectoCreateDto
        {
            Nombre = "Proyecto Test 1",
            Descripcion = "Descripción",
            FechaInicio = DateTime.Today,
            FechaFin = DateTime.Today.AddDays(10),
            UsuarioId = Guid.NewGuid()
        };

        _mockRepoUsuario.Setup(r => r.ObtenerPorIdActivoAsync(dto.UsuarioId))
            .ReturnsAsync(new Usuario { UsuarioId = dto.UsuarioId });

        Proyecto? proyectoAgregado = null;

        _mockRepoProyecto.Setup(r => r.AgregarAsync(It.IsAny<Proyecto>()))
            .Callback<Proyecto>(p => proyectoAgregado = p)
            .Returns(Task.CompletedTask);

        _mockRepoProyecto.Setup(r => r.GuardarCambiosAsync())
            .Returns(Task.CompletedTask);

      
        var result = await _servicio.CrearAsync(dto);

    
        Assert.NotNull(result);
        Assert.Equal(dto.Nombre, result.Nombre);
        Assert.Equal(dto.UsuarioId, result.UsuarioId);
        Assert.NotEqual(Guid.Empty, result.ProyectoId);
        Assert.Equal("Activo", proyectoAgregado?.Estado);
    }
    [Fact]
    public async Task CrearAsync_DeberiaLanzarExcepcion_CuandoUsuarioNoExiste()
    {
        
        var dto = new ProyectoCreateDto
        {
            Nombre = "Proyecto Test 2",
            FechaInicio = DateTime.Today,
            FechaFin = DateTime.Today.AddDays(5),
            UsuarioId = Guid.NewGuid()
        };

        _mockRepoUsuario.Setup(r => r.ObtenerPorIdActivoAsync(dto.UsuarioId))
            .ReturnsAsync((Usuario?)null);

       
        await Assert.ThrowsAsync<ExcepcionUsuarioNoEncontrado>(() => _servicio.CrearAsync(dto));
    }

}
