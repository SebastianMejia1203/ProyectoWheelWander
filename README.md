# WheelWander - Motorcycle Rental System 🏍️

WheelWander es una solución web de nivel empresarial desarrollada en **ASP.NET Core MVC**. El sistema está diseñado para digitalizar el proceso de alquiler de motocicletas, facilitando la interacción entre clientes y proveedores de servicios de movilidad.

## ✨ Características Principales

### 👤 Para Clientes
* **Catálogo Dinámico:** Exploración de motocicletas disponibles con filtros por marca y especificaciones.
* **Sistema de Reservas:** Proceso de reserva intuitivo con cálculo automático de costos.
* **Gestión de Perfil:** Registro, inicio de sesión y actualización de datos personales.
* **Notificaciones:** Confirmaciones vía correo electrónico integradas.

### 🛡️ Para Administradores
* **Gestión de Inventario (CRUD):** Registro, edición e inhabilitación de motocicletas en la flota.
* **Control de Usuarios:** Administración de cuentas y roles (Admin/Cliente).
* **Dashboard de Ganancias:** Visualización de estadísticas financieras y métricas del negocio.
* **Reportes Avanzados:** * Exportación de datos de usuarios a **Excel**.
    * Generación de comprobantes de reserva en **PDF**.
* **Gestión Multimedia:** Integración con la API de ImgBB para el almacenamiento de imágenes de la flota en la nube.

## 🛠️ Tecnologías Utilizadas

* **Framework:** .NET 8.0 / ASP.NET Core MVC
* **Lenguaje:** C#
* **Base de Datos:** Microsoft SQL Server
* **Acceso a Datos:** Entity Framework Core
* **Frontend:** HTML5, CSS3, JavaScript, jQuery, Bootstrap 5
* **Servicios Externos:** * **ImgBB API** (Carga de imágenes)
    * **SMTP Service** (Envío de correos)
    * **EPPlus / ClosedXML** (Generación de Excel)

## 📂 Estructura del Proyecto

* `/Controllers`: Lógica de navegación y procesamiento de solicitudes.
* `/Datos`: Capa de persistencia y conexión a SQL Server.
* `/Models`: Modelos de datos y ViewModels para la transferencia de información.
* `/Views`: Vistas desarrolladas con el motor Razor (CSHTML).
* `/Services`: Servicios inyectados para manejo de Email e Imágenes.
* `/wwwroot`: Archivos estáticos (CSS personalizado, JS y multimedia).

## ⚙️ Configuración e Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/sebastianmejia1203/proyectowheelwander.git](https://github.com/sebastianmejia1203/proyectowheelwander.git)
