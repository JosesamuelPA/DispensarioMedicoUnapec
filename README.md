# Dispensario Médico UNAPEC

Sistema de gestión de inventario médico, registro de pacientes, visitas y reportes diseñado para el dispensario de UNAPEC. Modernizado con Bootstrap 5, búsqueda inteligente y persistencia de datos robusta.

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

1.  **[.NET SDK 10.0](https://dotnet.microsoft.com/download)**: El framework principal del proyecto.
2.  **[SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)**: Se recomienda SQL Server Express o tener habilitado **LocalDB** (que viene con Visual Studio).
3.  **Entity Framework Core CLI**: Para gestionar las migraciones de base de datos.
    *   Ejecuta: `dotnet tool install --global dotnet-ef`

---

## 🚀 Pasos para la Configuración

Sigue estos pasos para poner a funcionar el proyecto en tu máquina local:

### 1. Clonar el Proyecto
```bash
git clone https://github.com/JosesamuelPA/DispensarioMedicoUnapec.git
cd DispensarioMedicoUnapec
```

### 2. Configurar la Base de Datos
Abre el archivo `appsettings.json` y verifica la cadena de conexión:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DispensarioMedicoUnapec;Trusted_Connection=True;MultipleActiveResultSets=true"
```
*   Si usas una instancia diferente de SQL Server, ajusta el valor de `Server`.
*   Nota: La doble barra invertida `\\` es necesaria por el formato JSON.

### 3. Restaurar Dependencias
Descarga todos los paquetes necesarios de NuGet:
```bash
dotnet restore
```

### 4. Aplicar Migraciones
Este comando creará la base de datos y las tablas físicamente en tu SQL Server:
```bash
dotnet ef database update
```

### 5. Iniciar la Aplicación
Finalmente, corre el proyecto:
```bash
dotnet run
```
*   La aplicación estará disponible en `http://localhost:5000` o la URL que indique la consola.