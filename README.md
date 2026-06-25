# Mediacrit Review

Mediacrit Review es una aplicación web desarrollada en **ASP.NET Core MVC** diseñada para gestionar/administrarun catálogo de contenido multimedia (películas, series, etc.), usuarios y reseñas. El sistema ofrece un entorno abierto y dinámico donde se pueden registrar opiniones de manera directa.

## 🚀 Características Principales

* **CRUD Completo de Contenido:** Registro, modificación, listado y eliminación de Modelos.
* **Sistema de Reseñas:** Flujo relacional que permite conectar las opiniones con sus respectivos creadores y medios de destino.
* **Validación de Datos Completa:** Validaciones aplicadas mediante scripts en atributos de modelo.

## 🛠️ Stack Tecnologico

* **Frontend:** HTML5, CSS3, JavaScript, Bootstrap 5, jQuery y jQuery Validation.
* **Backend:** .NET / ASP.NET Core MVC.
* **Acceso a Datos:** Entity Framework Core utilizando **Pomelo.EntityFrameworkCore.MySql**.
* **Base de Datos:** MySQL.

## 📁 Arquitectura del Proyecto

El proyecto sigue el patrón arquitectónico estándar de ASP.NET Core MVC:

* `Controllers/`: Contiene los controladores (`HomeController`, `UsersController`, `MediaController`, `ReviewsController`).
* `Models/`: Entidades del negocio (`User`, `Media`, `Review`).
* `Views/`: Almacena las interfaces de usuario, diseño unificado en la carpeta `Shared/`.
* `Data/`: Contiene el `ApplicationDbContext`, el puente para interactuar de forma relacional con MySQL.
* `wwwroot/`: Carpeta pública de distribución de archivos estáticos (estilos CSS, frameworks visuales y scripts).

---

## Requisitos previos

1. El SDK de **.NET 8.0** o superior.
2. **MySQL** local activo (Laragon, XAMPP o Workbench).

---

## 🛠️ Configuración e Instalación

### 1. Clonar o descargar el repositorio
Navega hasta la carpeta raíz donde deseas desplegar el software.

### 2. Configura la conexión en `db.js` si tu MySQL usa usuario o contraseña distintos:
Abre el archivo `appsettings.json` en la raíz de la aplicación y edita la credencial de acceso para que apunte a tu servidor MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MediacritReviewDb;User=root;Password=tu_contraseña;"
}
```

### 3. Restaurar Dependencias de NuGet:
Ejectuar el siguiente comando para instalar los paquetes requeridos, incluyendo el conector de Pomelo MySQL

```bash
dotnet restore
```

### 4. Aplicar Migraciones de la BD

```bash
dotnet ef database update
```

### 5. Ejecutar la aplicación

```bash
dotnet run
```