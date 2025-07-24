-- Crear la base de datos
--drop database GestorActividadesDB;
CREATE DATABASE GestorActividadesDB;
GO

USE GestorActividadesDB;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    UsuarioId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    NombreCompleto NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Telefono NVARCHAR(20),
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Admin', 'Editor', 'Viewer')),
    Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Activo', 'Inactivo','Eliminado')),
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaModificacion DATETIME NULL,
);
GO

-- Tabla: Proyectos
CREATE TABLE Proyectos (
    ProyectoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Activo', 'Inactivo','Eliminado')),
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaModificacion DATETIME NULL,

    CONSTRAINT FK_Proyectos_Usuarios FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios (UsuarioId)
        ON DELETE CASCADE
);
GO

-- Tabla: Actividades
CREATE TABLE Actividades (
    ActividadId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Fecha DATE NOT NULL,
    HorasEstimadas DECIMAL(5, 2) NOT NULL CHECK (HorasEstimadas >= 0),
    HorasReales DECIMAL(5, 2) NULL CHECK (HorasReales >= 0),
    Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Activo', 'Inactivo','Eliminado')),
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaModificacion DATETIME NULL,
    ProyectoId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Actividades_Proyectos FOREIGN KEY (ProyectoId)
        REFERENCES Proyectos (ProyectoId)
        ON DELETE CASCADE
);
GO

-- Inserts de prueba
INSERT INTO Usuarios (NombreCompleto, Correo, Telefono, Rol, Estado)
VALUES 
('Ana Torres', 'ana.torres@example.com', '0991234567', 'Admin', 'Activo'),
('Luis Méndez', 'luis.mendez@example.com', '0987654321', 'Editor', 'Activo'),
('Carmen Paredes', 'carmen.paredes@example.com', '0971122334', 'Viewer', 'Activo'),
('Marco Ruiz', 'marco.ruiz@example.com', '0964433221', 'Editor', 'Inactivo'),
('Daniela Bravo', 'daniela.bravo@example.com', '0998877665', 'Viewer', 'Activo');


DECLARE @UsuarioId1 UNIQUEIDENTIFIER = (SELECT UsuarioId FROM Usuarios WHERE Correo = 'ana.torres@example.com');
DECLARE @UsuarioId2 UNIQUEIDENTIFIER = (SELECT UsuarioId FROM Usuarios WHERE Correo = 'luis.mendez@example.com');
DECLARE @UsuarioId3 UNIQUEIDENTIFIER = (SELECT UsuarioId FROM Usuarios WHERE Correo = 'carmen.paredes@example.com');
DECLARE @UsuarioId4 UNIQUEIDENTIFIER = (SELECT UsuarioId FROM Usuarios WHERE Correo = 'marco.ruiz@example.com');
DECLARE @UsuarioId5 UNIQUEIDENTIFIER = (SELECT UsuarioId FROM Usuarios WHERE Correo = 'daniela.bravo@example.com');


INSERT INTO Proyectos (Nombre, Descripcion, FechaInicio, FechaFin, Estado, UsuarioId)
VALUES
('Sistema de Inventario', 'Proyecto para gestión de inventario con QR', '2024-02-01', '2024-05-30', 'Activo', @UsuarioId1),
('App de Ventas', 'Aplicación móvil para registrar ventas en campo', '2024-03-10', '2024-07-15', 'Activo', @UsuarioId2),
('Rediseño Web', 'Rediseño del portal corporativo institucional', '2024-04-01', '2024-06-30', 'Activo', @UsuarioId3),
('Dashboard KPIs', 'Panel visual de KPIs financieros', '2024-01-15', '2024-04-15', 'Inactivo', @UsuarioId4),
('Control de Asistencia', 'Sistema web para gestionar asistencia de personal', '2024-05-01', '2024-08-30', 'Activo', @UsuarioId5);

-- Obtener y guardar los IDs de los proyectos recién insertados
DECLARE @ProyectoId1 UNIQUEIDENTIFIER = (SELECT ProyectoId FROM Proyectos WHERE Nombre = 'Sistema de Inventario');
DECLARE @ProyectoId2 UNIQUEIDENTIFIER = (SELECT ProyectoId FROM Proyectos WHERE Nombre = 'App de Ventas');
DECLARE @ProyectoId3 UNIQUEIDENTIFIER = (SELECT ProyectoId FROM Proyectos WHERE Nombre = 'Rediseño Web');
DECLARE @ProyectoId4 UNIQUEIDENTIFIER = (SELECT ProyectoId FROM Proyectos WHERE Nombre = 'Dashboard KPIs');
DECLARE @ProyectoId5 UNIQUEIDENTIFIER = (SELECT ProyectoId FROM Proyectos WHERE Nombre = 'Control de Asistencia');

-- Insertar Actividades con variables
INSERT INTO Actividades (Titulo, Descripcion, Fecha, HorasEstimadas, HorasReales, Estado, ProyectoId)
VALUES
('Diseñar estructura de base de datos', 'Definir tablas y relaciones principales', '2024-02-02', 6.0, 6.5, 'Activo', @ProyectoId1),
('Maquetar interfaz principal', 'HTML/CSS inicial del sistema', '2024-03-11', 4.0, 4.0, 'Activo', @ProyectoId2),
('Revisión con el cliente', 'Feedback sobre el diseño web', '2024-04-15', 2.0, 2.5, 'Activo', @ProyectoId3),
('Carga de datos de prueba', 'Ingreso de datos dummy en dashboard', '2024-01-20', 3.0, 3.0, 'Inactivo', @ProyectoId4),
('Configurar control de horarios', 'Asignación de turnos y validación', '2024-05-03', 5.5, NULL, 'Activo', @ProyectoId5);

