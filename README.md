# Proyectos de Informática: Laboratorio Clinico 

En este proyecto se pusieron a prueba varios de los conocimientos adquiridos sobre el lenguaje de programación C# para hacer un software capas de solventar problemáticas del Laboratorio Clínico Inmunohematolico Escobar, y a la vez facilitar alguna de las operaciones que se realizan en este laboratorio clínico.

Algunas de las cosas que se aprendieron en este proyecto son:
-	Implementación de Entity Framework con un mapeo Database First, que básicamente es una implementación de Entity Framework que se basa en tener una base de datos diseñada con anterioridad para poder crear los archivos necesarios para conectarnos y administrar la base de datos. En este caso el motor de base de datos que se utilizó fue Microsoft SQL Server.
-	También se trabajo con herramientas para poder conectar con el gestor de correos de Gmail para enviar códigos de validación, a la hora de la recuperación de contraseñas de un usuario previamente registrado en nuestra base de datos.
-	Se implementaron funcionalidades extras como la de hacer un respaldo total de la base de datos, como también poder cargar cualquiera de los respaldos anteriores a nuestro sistema, esto se hace con el fin de tener la información respaldada y poder volver a hacer eso de ella en cualquier momento que se necesite.
-	Utilización de PrintDocument() una clase que nos proporciona funcionalidades para manejar impresiones de formularios, para este proyecto se utilizó para la impresión de los resultados de los exámenes.
-	Integrar el sistema de versionamiento de GitHub en Visual Studio para llevar un control de todos los cambios realizados en el proyecto.
-	Generación del archivo ejecutable como las configuraciones básicas para poder implementar el sistema en un entorno real.

### Diagrama Entidad Relación (DER).
![Diagrama Entidad Relacion](https://github.com/Aviscad/Lab/blob/master/DER-labclinico.png)

### Vistas de Algunas de las Interfaces del Software

#### Login
![login](https://github.com/Aviscad/Lab/blob/master/img/login.png)

#### Vista de Usuarios
![usuarios](https://github.com/Aviscad/Lab/blob/master/img/usuarios.png)

#### Vista de Pacientes
![pacientes](https://github.com/Aviscad/Lab/blob/master/img/pacientes.png)

#### Vista de Exámenes
![examenes](https://github.com/Aviscad/Lab/blob/master/img/examenes.png)

#### Vista de Modificar de Exámenes
![modificarexamenes](https://github.com/Aviscad/Lab/blob/master/img/modificar-examen.png)

#### Vista Previa de Impresión de Exámenes
![vistaimpresion](https://github.com/Aviscad/Lab/blob/master/img/vista-impresion.png)

#### Formulario de Reseteo de Contraseña
![resetpassword](https://github.com/Aviscad/Lab/blob/master/img/reset-password.png)

