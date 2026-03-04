# ⚽ Sistema de Reserva de Canchas de Fútbol

## 📌 Descripción

Sistema integral para la gestión y reserva de canchas de fútbol desarrollado para la materia Seminario I de la carrera Analista Universitario en Sistemas, dictada en el Instituto Politécnico Superior “Gral. San Martín” de Rosario.

La aplicación permite:

* Registrar usuarios
* Autenticarse mediante JWT
* Reservar canchas en franjas horarias
* Gestionar disponibilidad
* Administrar reservas
* Controlar concurrencia para evitar doble reserva de un mismo turno

El sistema está compuesto por:

* 🌐 API REST
* 💻 Aplicación Web (Frontend)
* 🖥 Aplicación de Escritorio (Windows Forms)

---

## 🏗 Arquitectura

El sistema sigue una arquitectura cliente-servidor:

* **Backend:** API REST desarrollada en .NET
* **Frontend Web:** Angular
* **App Escritorio:** Windows Forms
* **Base de datos:** PostgreSQL
* **Control de concurrencia:** Redis
* **Autenticación:** JWT

---

## 🚀 Tecnologías Utilizadas

### Backend

* .NET
* Entity Framework (ORM)
* JWT (Autenticación y Autorización)
* PostgreSQL
* Redis

### Frontend

* Angular

### Desktop

* Windows Forms

---

# 🚀 Cómo Levantar el Proyecto

---

# 📋 Requisitos Previos

Instalar previamente:

* .NET SDK 8+
* Node.js y npm
* Angular CLI
* PostgreSQL
* Redis
* Visual Studio (para Windows Forms)

---

# ⚙️ Configuración del Backend (.NET API)

## 1️⃣ Configurar archivo de entorno

El proyecto incluye:

* `appsettings.json` → Configuración base sin credenciales
* `appsettings.Example.json` → Archivo guía

### 📌 Crear archivo local

En la carpeta `ReservasCanchas/Controller`:

1. Copiar:

```
appsettings.Example.json
```

2. Renombrar a:

```
appsettings.Development.json
```

3. Completar con tus credenciales locales:

```json
{
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Port=5432;Database=reservefieldDB;Username=postgres;Password=TU_PASSWORD",
    "Redis": "localhost:6379"
  },
  "JWT": {
    "Issuer": "https://localhost:7004",
    "Audience": "https://localhost:7004",
    "SignInKey": "CLAVE_SUPER_SECRETA_MINIMO_64_CARACTERES"
  }
}
```

⚠️ Este archivo no debe subirse al repositorio.

---

## 2️⃣ Crear Base de Datos

Ingresar a PostgreSQL y ejecutar:

```sql
CREATE DATABASE reservefieldDB;
```

---

## 3️⃣ Ejecutar Migraciones

Desde la carpeta raíz:

```bash
cd ReservasCanchas.DataAccess
dotnet ef database update
```

Esto aplicará las migraciones ubicadas en:

```
ReservasCanchas.DataAccess/Migrations
```

---

## 4️⃣ Verificar Redis

Asegurarse de que Redis esté corriendo en:

```
localhost:6379
```

---

## 5️⃣ Levantar la API

Desde la carpeta del proyecto principal (Controllers):

```bash
cd ReservasCanchas.Controllers
dotnet run
```

La API quedará disponible en:

```
https://localhost:7004
```

Por defecto el entorno es:

```
ASPNETCORE_ENVIRONMENT = Development
```

Por lo tanto .NET cargará automáticamente:

```
appsettings.json
+
appsettings.Development.json
```

---

# 🌐 Levantar el Frontend (Angular)

## 6️⃣ Instalar dependencias

```bash
cd frontend/reservar-canchas
npm install
```

## 7️⃣ Ejecutar aplicación

```bash
ng serve
```

Disponible en:

```
http://localhost:4200
```

---

# 🖥 Levantar la App de Escritorio

1. Abrir la solución en Visual Studio.
2. Seleccionar el proyecto:

```
ReservasCanchas.WinFormsAppDeEscritorio
```

3. Ejecutar la aplicación.

⚠️ La API debe estar corriendo previamente.

---

# ✅ Orden Correcto de Ejecución

1. Iniciar PostgreSQL
2. Iniciar Redis
3. Ejecutar migraciones
4. Levantar API
5. Levantar Frontend o App de Escritorio

---

## 🔐 Autenticación

El sistema utiliza JWT (JSON Web Tokens).

Flujo:

1. El usuario se autentica enviando email y contraseña.
2. La API devuelve un token JWT.
3. El cliente (Web o Desktop) envía el token en cada request:

```
Authorization: Bearer {token}
```

La API valida el token y autoriza el acceso según los permisos del usuario.

---

## 🔄 Control de Concurrencia

Para evitar reservas duplicadas en el mismo horario:

* Se utiliza Redis como mecanismo de lock distribuido.
* Antes de confirmar una reserva:

  * Se genera un lock temporal de 15 minutos por cancha + dia + horario.
  * Si el lock ya existe (otro usuario esta reservando esa cancha, en ese día y horario), no se le permite a otro usuario realizar la reserva.
  * Si no existe, se crea el lock y se redirige al checkout.
  * Dentro del lapso de 15 minutos el usuario puede confirmar la reserva, en tal caso se elimina el lock de Redis, y se crea la reserva en la base de datos.
  * Si pasado los 15 minutos el usuario no confirmo la reserva, o la cancelo, el lock se libera y la cancha en ese día y horario queda liberada para ser reservada por otro usuario.

Esto evita condiciones de carrera cuando múltiples usuarios intentan reservar el mismo turno simultáneamente.

