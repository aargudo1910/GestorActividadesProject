using GestorActividades.DTOs.Usuario;
using GestorActividades.Entidades;
using GestorActividades.Excepciones.Usuarios;
using GestorActividades.Repositorios.UsuarioRepositorio;
using GestorActividades.Servicios.UsuarioServicio;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GestorActividades.Tests
{
    public class ServicioUsuarioTests
    {
        private readonly Mock<IRepositorioUsuario> _mockRepo;
        private readonly Mock<ILogger<ServicioUsuario>> _mockLogger;
        private readonly ServicioUsuario _servicio;

        public ServicioUsuarioTests()
        {
            _mockRepo = new Mock<IRepositorioUsuario>();
            _mockLogger = new Mock<ILogger<ServicioUsuario>>();
            _servicio = new ServicioUsuario(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CrearAsync_DeberiaCrearUsuario_CuandoCorreoNoExiste()
        {
            
            var dto = new UsuarioCreateDto
            {
                NombreCompleto = "Juan Pablo",
                Correo = "juan@gmail.com",
                Telefono = "0988765674",
                Rol = "Admin"
            };

            _mockRepo.Setup(r => r.BuscarPorCorreoAsync(dto.Correo))
                     .ReturnsAsync((Usuario?)null);

          
            var resultado = await _servicio.CrearAsync(dto);

        
            Assert.Equal(dto.NombreCompleto, resultado.NombreCompleto);
            Assert.Equal(dto.Correo, resultado.Correo);
            Assert.Equal(dto.Rol, resultado.Rol);
            _mockRepo.Verify(r => r.AgregarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task CrearAsync_DeberiaLanzarExcepcionCorreoDuplicado()
        {
            
            var dto = new UsuarioCreateDto
            {
                NombreCompleto = "Pedro Peréz",
                Correo = "prueba@email.com",
                Telefono = "0972683787",
                Rol = "Editor"
            };

            _mockRepo.Setup(r => r.BuscarPorCorreoAsync(dto.Correo))
                     .ReturnsAsync(new Usuario { Correo = dto.Correo });

           
            await Assert.ThrowsAsync<ExcepcionCorreoDuplicado>(() => _servicio.CrearAsync(dto));
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaActualizarUsuario_CuandoDatosValidos()
        {
            
            var id = Guid.NewGuid();
            var dto = new UsuarioUpdateDto
            {
                NombreCompleto = "Nuevo Nombre Pablo",
                Correo = "pablo@email.com",
                Telefono = "0986253725",
                Rol = "Viewer"
            };

            var usuarioExistente = new Usuario
            {
                UsuarioId = id,
                NombreCompleto = "Antiguo",
                Correo = "anterior@correo.com",
                Estado = "Activo"
            };

            _mockRepo.Setup(r => r.ObtenerPorIdActivoAsync(id))
                     .ReturnsAsync(usuarioExistente);

            _mockRepo.Setup(r => r.BuscarPorCorreoAsync(dto.Correo))
                     .ReturnsAsync((Usuario?)null);

          
            var resultado = await _servicio.ActualizarAsync(id, dto);

        
            Assert.True(resultado);
            Assert.Equal(dto.Correo, usuarioExistente.Correo);
            Assert.Equal(dto.NombreCompleto, usuarioExistente.NombreCompleto);
            _mockRepo.Verify(r => r.Actualizar(usuarioExistente), Times.Once);
            _mockRepo.Verify(r => r.GuardarCambiosAsync(), Times.Once);
        }


        [Fact]
        public async Task ActualizarAsync_DeberiaLanzarExcepcionCorreoDuplicado_CuandoCorreoYaExiste()
        {
            
            var id = Guid.NewGuid();
            var dto = new UsuarioUpdateDto
            {
                NombreCompleto = "Nuevo Nombre",
                Correo = "repetido@gmail.com",
                Telefono = "09872536475",
                Rol = "Admin"
            };

            var usuarioActual = new Usuario
            {
                UsuarioId = id,
                Correo = "original@gmail.com",
                Estado = "Activo"
            };

            var otroUsuario = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Correo = "repetido@gmail.com"
            };

            _mockRepo.Setup(r => r.ObtenerPorIdActivoAsync(id))
                     .ReturnsAsync(usuarioActual);

            _mockRepo.Setup(r => r.BuscarPorCorreoAsync(dto.Correo))
                     .ReturnsAsync(otroUsuario);

           
            await Assert.ThrowsAsync<ExcepcionCorreoDuplicado>(() =>
                _servicio.ActualizarAsync(id, dto));
        }

    }
}
