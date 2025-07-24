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
    Estado BIT NOT NULL DEFAULT 1
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
    ProyectoId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Actividades_Proyectos FOREIGN KEY (ProyectoId)
        REFERENCES Proyectos (ProyectoId)
        ON DELETE CASCADE
);
GO
