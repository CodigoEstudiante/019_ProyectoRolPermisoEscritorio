
 USE DB_PERMISOS01


--PROCEDMIENTO PARA OBTENER USUARIO

create procedure usp_LoginUsuario(
@Usuario varchar(60),
@Clave varchar(60),
@IdUsuario int output
)
as
begin
	set @IdUsuario = 0
	if exists(
	select * from USUARIOS 
	where Usuario COLLATE Latin1_General_CS_AS = @Usuario 
	and Clave COLLATE Latin1_General_CS_AS = @Clave
	)

	set @IdUsuario = (
	select IdUsuario from USUARIOS 
	where Usuario COLLATE Latin1_General_CS_AS = @Usuario 
	and Clave COLLATE Latin1_General_CS_AS = @Clave
	)

end

 --MENUS
 SELECT DISTINCT M.*
 FROM PERMISO P
 JOIN ROL R ON R.IdRol = P.IdRol
 JOIN SUBMENU SM ON SM.IdSubMenu = P.IdSubMenu
 JOIN MENU M ON M.IdMenu = SM.IdMenu
 JOIN USUARIOS U ON U.IdRol = P.IdRol
 where U.IdUsuario = 2
 

 --SUB MENUS
 SELECT SM.*,P.Activo
 FROM PERMISO P
 JOIN ROL R ON R.IdRol = P.IdRol
 JOIN SUBMENU SM ON SM.IdSubMenu = P.IdSubMenu
 JOIN USUARIOS U ON U.IdRol = P.IdRol
 where U.IdUsuario = 2


 --PROCEDMIENTO PARA OBTENER DETALLE USUARIO
create proc usp_ObtenerPermisos(
@IdUsuario int
)
as
begin

select
 (

  select vistaMenu.Nombre,vistaMenu.Icono,
  
   (SELECT SM.Nombre,sm.NombreFormulario
	 FROM PERMISO P
	 JOIN ROL R ON R.IdRol = P.IdRol
	 JOIN SUBMENU SM ON SM.IdSubMenu = P.IdSubMenu
	 JOIN USUARIOS U ON U.IdRol = P.IdRol
	 where U.IdUsuario = US.IdUsuario and vistaMenu.IdMenu = sm.IdMenu
	 FOR XML PATH ('SubMenu'),TYPE) AS 'DetalleSubMenu' 
  
   from (
	SELECT DISTINCT M.*
	 FROM PERMISO P
	 JOIN ROL R ON R.IdRol = P.IdRol
	 JOIN SUBMENU SM ON SM.IdSubMenu = P.IdSubMenu
	 JOIN MENU M ON M.IdMenu = SM.IdMenu
	 JOIN USUARIOS U ON U.IdRol = P.IdRol
	 where U.IdUsuario = US.IdUsuario and p.Activo = 1
	 ) vistaMenu
	 FOR XML PATH ('Menu'),TYPE) AS 'DetalleMenu' 


from USUARIOS US
WHERE us.IdUsuario = @IdUsuario
FOR XML PATH(''), ROOT('PERMISOS') 
end