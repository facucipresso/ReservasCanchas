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

## 📦 Requisitos Previos

Antes de ejecutar el proyecto necesitás tener instalado:

* .NET SDK
* Node.js y npm
* Angular CLI
* PostgreSQL
* Redis
* Visual Studio (para Windows Forms)

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

