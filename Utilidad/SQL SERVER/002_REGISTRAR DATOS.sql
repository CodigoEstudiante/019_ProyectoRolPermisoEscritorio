
 USE DB_PERMISOS01


 SELECT * FROM MENU
 SELECT * FROM SUBMENU


 INSERT INTO MENU(IdMenu,Nombre,Icono) VALUES 
 (1,'Usuarios','\Iconos\Usuario.png'),
 (2,'Ventas','\Iconos\Ventas.png'),
 (3,'Reportes','\Iconos\Reportes.png')

 --MENU USUARIOS
 INSERT INTO SUBMENU(IdSubMenu,IdMenu,Nombre,NombreFormulario) VALUES
 (1,1,'Crear Usuario','frmCrearUsuario'),
 (2,1,'Editar Usuario','frmEditarUsuario'),
 (3,1,'Eliminar Usuario','frmEliminarUsuario')

  --MENU VENTAS
 INSERT INTO SUBMENU(IdSubMenu,IdMenu,Nombre,NombreFormulario) VALUES
 (4,2,'Crear Venta','frmCrearVenta'),
 (5,2,'Editar Venta','frmEditarVenta')

  --MENU REPORTES
 INSERT INTO SUBMENU(IdSubMenu,IdMenu,Nombre,NombreFormulario) VALUES
 (6,3,'Reporte Ventas','frmReporteVenta'),
 (7,3,'Reporte Cliente','frmReporteCliente')


 select * from MENU m
 inner join submenu sb on m.IdMenu = sb.idmenu


 INSERT INTO ROL(IdRol,Nombre) VALUES 
 (1,'ADMINISTRADOR'),
 (2,'EMPLEADO')


 SELECT * FROM PERMISO

 --PERMISOS ADMINISTRADOR
 INSERT INTO PERMISO(IdRol,IdSubMenu,Activo) VALUES
 (1,1,1),
 (1,2,1),
 (1,3,1),
 (1,4,1),
 (1,5,1),
 (1,6,1),
 (1,7,1)

 --PERMISOS EMPLEADO
  INSERT INTO PERMISO(IdRol,IdSubMenu,Activo) VALUES
 (2,1,0),
 (2,2,0),
 (2,3,0),
 (2,4,1),
 (2,5,1),
 (2,6,0),
 (2,7,0)

 SELECT * FROM USUARIOS

 INSERT INTO USUARIOS(Nombres,Usuario,Clave,IdRol) VALUES
 ('PERSONA 01','ADMIN','123',1),
 ('PERSONA 02','EMP','456',2)