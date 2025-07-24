# API .NET 8 + SQL Server

### 1. Clonar repositorio

```bash
git clone https://github.com/aargudo1910/GestorActividadesProject.git
cd GestorActividadesProject/GestorActividades
```

### 2. Base de datos

```bash
# Ejecutar script SQL en SQL Server Management Studio
BD/BaseDatos.sql
```

### 3. Configurar conexión

Editar `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=[SERVER];Database=[DATABASE];Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 4. Ejecutar API

```bash
dotnet restore
dotnet run
```

La API estará disponible en `https://localhost:7107`

### 5. Probar endpoints

#### Swagger UI

Navegar a: `https://localhost:7107/swagger`

#### Postman

Importar colección: `GestorActividades_postman_collection.json`

### 6. Tests unitarios

```bash
dotnet test
```

---

## 📋 Requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio Code / Visual Studio
