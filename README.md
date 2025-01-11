# 🌐 Catedra 3: Introducción al desarrollo web/móvil

## 📌 Integrantes
* Fernando Chavez B. (21180530-7)


## 📖 Descripción

Este es un proyecto escrito en el lenguaje de programación C# con el entorno .NET el cual es una coledcción de controladores
para poder manipular Post y usuarios de una pagina para Post.
---
## Tecnologías
El proyecto utiliza las siguientes tecnologías y herramientas:
- **C#**: Lenguaje de programación.  
- **.NET 8**: Framework para construir la API REST.  
- **SQLite**: Base de datos para almacenar los datos del proyecto.  
- **Cloudinary**: Servicio de almacenamiento de imágenes en la nube.  
- **JWT**: Autenticación mediante tokens seguros.  
- **Postman**: Herramienta para pruebas y documentación de los endpoints.  

## ⚙️ Requisitos Previos

Asegúrate de tener instalado:
1. [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
2. [SQLite](https://www.sqlite.org/download.html)  
3. **Git** para clonar el repositorio.  
4. **Postman** (opcional, para probar los endpoints).  

---

## 🚀 Construcción

### 1️⃣ Clonar el Repositorio

Clonar el repositorio utilizando git
```bash
  git clone https://github.com/FernandoChav/https://github.com/FernandoChav/Catedra_3 
```
### 2️⃣ Ir a la carpeta que contiene el proyecto
```bash
  cd Catedra_3
```
---

### 3️⃣ Configuración del Archivo `appsettings.json`

El archivo `appsettings.json` contiene las configuraciones esenciales para el funcionamiento de la API.  
Asegúrate de que tenga la siguiente estructura:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Cloudinary": {
    "Url": "cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>"
  },
  "JWT": {
    "ValidAudience": "https://localhost:5026",
    "ValidIssuer": "https://localhost:5026",
    "Secret": "<YOUR_SECRET_KEY>"
  }
}
```
### 3️⃣ Restauracion de las dependencias 
```bash
  dotnet restore
```
---
#### 🔍 Explicación de Configuración

- **ConnectionStrings.DefaultConnection**:  
  Define la cadena de conexión para la base de datos SQLite (`app.db`).  
  - **Valor Ejemplo**: `"Data Source=app.db"`  
    Este valor indica que el archivo de base de datos `app.db` estará ubicado en la raíz del proyecto.
  - **Cómo Configurarlo**:  
    Si deseas cambiar la ubicación o el nombre del archivo, edita la cadena de conexión. Por ejemplo:
    ```json
    "Data Source=./Data/tienda.db"
    ```
  
- **Logging**:  
  Configura los niveles de los registros (logs) generados por la aplicación.  
  - **Propósito**: Ajustar los detalles de los logs (información, advertencias, errores, etc.).
  - **Valores comunes**:
    - `Information`: Información general sobre la ejecución del sistema.
    - `Warning`: Advertencias de comportamiento no esperado.
    - `Error`: Errores en el sistema que podrían necesitar atención.

- **AllowedHosts**:  
  Define los dominios desde los cuales se permite acceder a la API.  
  - **Valor Ejemplo**: `"*"`  
    El asterisco `"*"` permite acceso desde cualquier dominio. En entornos de producción, limita esta configuración a tus dominios específicos para mejorar la seguridad.

- **Cloudinary**:  
  Configura las credenciales necesarias para integrar la API con el servicio de almacenamiento de imágenes en la nube Cloudinary.  
  - **Propósito**: Subir y gestionar imágenes de productos en la nube.
  - **Url**:  
    - `<API_KEY>`: Tu clave de API.  
    - `<API_SECRET>`: Tu clave secreta de API.  
    - `<CLOUD_NAME>`: El nombre de tu cuenta de Cloudinary.
  - **Cómo Obtenerlas**:
    1. Crea una cuenta en [Cloudinary](https://cloudinary.com/).
    2. Accede al [Dashboard](https://cloudinary.com/console).
    3. Copia las credenciales de la sección "Account Details" para configurar correctamente la URL.

- **JWT**:  
  Configuración para la autenticación utilizando **JSON Web Tokens (JWT)**.  
  - **ValidAudience** y **ValidIssuer**: Estas son las URLs que identifican a los emisores y receptores válidos del token.
    - **Valor Ejemplo**: `"https://localhost:5026"`  
      Esto corresponde a la URL local donde la API está disponible.
  - **Secret**:  
    La clave secreta utilizada para firmar los tokens JWT y asegurar su integridad.
    - **Cómo Generarla**: Puedes generar una clave secreta utilizando el siguiente comando en la terminal:
      ```bash
      openssl rand -base64 32
      ```
    - **Valor Ejemplo**: `"ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"`

---

### 4️⃣ Migraciones de Base de Datos

Si estás utilizando **Entity Framework** para manejar la base de datos, debes aplicar las migraciones necesarias con los siguientes pasos:

1. **Generar las migraciones**:
   Ejecuta el siguiente comando para crear la migración inicial:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
Este comando generará un archivo de migración que define la estructura de la base de datos.

2. **Aplicar las migraciones para crear la base de datos:**
   Ejecuta el siguiente comando para aplicar la migración y crear la base de datos:
   ```bash
   dotnet ef database update
   ```
---
### 5️⃣ Ejecutar el Proyecto
  Una vez completados los pasos anteriores, puedes iniciar el servidor localmente con el siguiente comando:
 
 ```bash
   dotnet run
  ```
  