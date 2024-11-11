USE [LoginService]
GO

INSERT INTO [dbo].[Clientes]
           ([nombres]
           ,[apellidos]
           ,[fechanacimiento]
           ,[direccion]
           ,[password]
           ,[telefono]
           ,[email]
           ,[fechamodificacion])
     VALUES
           ('ricardo'
           ,'ramos'
           ,'1992-10-30'
           ,'San Salvador'
           ,'pass'
           ,'72668190'
           ,'rich_ard1030@hotmail.com'
           ,null)
GO


