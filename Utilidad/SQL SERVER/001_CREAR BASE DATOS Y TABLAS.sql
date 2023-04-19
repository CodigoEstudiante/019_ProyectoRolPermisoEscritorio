
CREATE DATABASE DB_PERMISOS01

GO

USE DB_PERMISOS01

GO

CREATE TABLE MENU(
IdMenu int primary key,
Nombre varchar(50),
Icono varchar(50)
)


GO

CREATE TABLE SUBMENU(
IdSubMenu int primary key,
IdMenu int references MENU(IdMenu),
Nombre varchar(50),
NombreFormulario varchar(50)
)

GO

CREATE TABLE ROL(
IdRol int primary key,
Nombre varchar(50)
)

GO

CREATE TABLE PERMISO(
IdPermiso int primary key identity,
IdRol int,
IdSubMenu int references SUBMENU(IdSubMenu),
Activo bit
)



GO

CREATE TABLE USUARIOS(
IdUsuario int  primary key identity,
Nombres varchar(50),
Usuario varchar(50),
Clave varchar(50),
IdRol int references ROL(IdRol)
)

