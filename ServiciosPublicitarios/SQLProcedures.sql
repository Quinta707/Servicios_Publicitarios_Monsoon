
USE ServiciosPublicitarios

--********** VISTAS Y PROCEDIMIENTOS  **********---


--***************************************--
--********* TABLA CATEGORIAS ************--


--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbCategorias
AS
SELECT  cate_Id, 
		cate_Descripcion, 
		T2.user_NombreUsuario AS user_Creacion,
		cate_UsuCreacion, 
		cate_FechaCreacion, 
		cate_UsuModificacion, 
		T3.user_NombreUsuario AS user_Modificacion,
		cate_FechaModificacion, 
		cate_Estado
FROM gral.tbCategorias AS T1 INNER JOIN acce.tbUsuarios AS T2
ON T1.cate_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.cate_UsuModificacion = T3.[user_Id];


--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCategorias_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbCategorias
	WHERE cate_Estado = 1;
END


--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCategorias_Insert
(@cate_Descripcion NVARCHAR(100),
 @cate_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
		INSERT INTO tbCategorias (cate_Descripcion, cate_UsuCreacion, cate_UsuModificacion, cate_FechaModificacion)
		VALUES(@cate_Descripcion, @cate_UsuCreacion, NULL, NULL);
		
		SELECT 1 CodeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 CodeStatus
	END CATCH
END

--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCategorias_Update
(@cate_Id INT,
 @cate_Descripcion NVARCHAR(100),
 @cate_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY 

		UPDATE	gral.tbCategorias
		SET		cate_Descripcion = @cate_Descripcion, 
				cate_UsuModificacion = @cate_UsuModificacion, 
				cate_FechaModificacion = GETDATE()
		WHERE	cate_Id = @cate_Id

		SELECT 1 CodeStatus
	END TRY
	BEGIN CATCH
		Select 0 CodeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCategorias_Delete
(@cate_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE	gral.tbCategorias
		SET		cate_Estado = 0
		WHERE	cate_Id = @cate_Id

		SELECT 1 CodeStatus
	END TRY 
	BEGIN CATCH
		SELECT 0 CodeStatus
	END CATCH
END


--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCategorias_Find
(@cate_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbCategorias
	WHERE cate_Id = @cate_Id;
END



--******************************************--
--********* TABLA DEPARTAMENTOS ************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbDepartamentos
AS
SELECT	depa_Id, 
		depa_Nombre, 
		depa_Codigo, 
		depa_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		depa_FechaCreacion, 
		depa_UsuModificacion, 
		T3.user_NombreUsuario AS user_Modificacion,
		depa_FechaModificacion, 
		depa_Estado
FROM [gral].[tbDepartamentos] AS T1 INNER JOIN [acce].[tbUsuarios] AS T2
ON T1.depa_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.depa_UsuModificacion = T3.[user_Id];


--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbDepartamentos_Insert
(@depa_Nombre NVARCHAR(100),
 @depa_UsuCreacion	INT)
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT * FROM gral.tbDepartamentos WHERE depa_Nombre = @depa_Nombre AND depa_Estado = 1)
			BEGIN
				SELECT 2 codeStatus
			END
		ELSE IF NOT EXISTS (SELECT * FROM gral.tbDepartamentos WHERE depa_Nombre = @depa_Nombre)
			BEGIN
				INSERT INTO [gral].[tbDepartamentos] (depa_Nombre, depa_Codigo, depa_UsuCreacion, depa_UsuModificacion, depa_FechaModificacion)
				VALUES (@depa_Nombre, (SELECT COUNT(depa_codigo) + 1 FROM gral.tbDepartamentos WHERE depa_Estado = 1), @depa_UsuCreacion, NULL, NULL);

				SELECT 1 codeStatus
			END
		ELSE 
			BEGIN
				UPDATE gral.tbDepartamentos
				SET depa_Nombre = @depa_Nombre, 
					depa_UsuCreacion = @depa_UsuCreacion, 
					depa_FechaCreacion = GETDATE(), 
					depa_UsuModificacion = NULL, 
					depa_FechaModificacion = NULL, 
					depa_Estado = 1
				WHERE depa_Nombre = @depa_Nombre

				SELECT 1 codeStatus
			END
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbDepartamentos_Update
(@depa_Id INT,
 @depa_Nombre NVARCHAR(100),
 @depa_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT * FROM gral.tbDepartamentos WHERE (depa_Nombre = @depa_Nombre AND depa_Id != @depa_Id))
			BEGIN
				SELECT 2 codeStatus
			END
		ELSE
			BEGIN
				UPDATE gral.tbDepartamentos
				SET   depa_Nombre = @depa_Nombre,  
					  depa_UsuModificacion = @depa_UsuModificacion, 
					  depa_FechaModificacion = GETDATE()
				WHERE depa_Id = @depa_Id		

				SELECT 1 codeStatus
			END 
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbDepartamentos_Delete
(@depa_Id INT)
AS
BEGIN
 BEGIN TRY
	UPDATE	gral.tbDepartamentos
	SET		[depa_Estado] = 0
	WHERE	depa_Id = @depa_Id

	SELECT 1 codeStatus
 END TRY
 BEGIN CATCH
	SELECT 0 codeStatus
 END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbDepartamentos_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbDepartamentos 
	WHERE depa_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbDepartamentos_Find 
(@depa_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbDepartamentos
	WHERE depa_Id = @depa_Id
END




--******************************************--
--************ TABLA MUNICIPIOS ************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbMunicipios
AS
SELECT	muni_Id, 
		muni_Nombre, 
		muni_Codigo, 
		T1.depa_Id, 
		T2.depa_Nombre
		muni_UsuCreacion, 
		T3.user_NombreUsuario AS user_Creacion,
		muni_FechaCreacion, 
		muni_UsuModificacion, 
		t4.user_NombreUsuario AS user_Modificacion,
		muni_FechaModificacion, 
		muni_Estado
FROM gral.tbMunicipios AS T1 INNER JOIN gral.tbDepartamentos AS T2
ON T1.depa_Id = T2.depa_Id INNER JOIN acce.tbUsuarios AS T3
ON T1.muni_UsuCreacion = t3.user_Id LEFT JOIN acce.tbUsuarios AS T4
ON T1.muni_UsuModificacion = t4.user_Id



--**************  CREATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMunicipios_Insert
(@muni_Nombre NVARCHAR(100),
 @muni_Codigo char(4),
 @depa_Id INT,
 @muni_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT * FROM gral.tbMunicipios WHERE muni_Codigo = @muni_Codigo AND muni_Estado = 1)
			BEGIN
				SELECT 2 codeStatus
			END
		ELSE IF NOT EXISTS (SELECT * FROM gral.tbMunicipios WHERE muni_Codigo = @muni_Codigo)
			BEGIN
				INSERT INTO [gral].[tbMunicipios] (muni_Nombre, muni_Codigo, depa_Id, muni_UsuCreacion, muni_UsuModificacion, muni_FechaModificacion)
				VALUES (@muni_Nombre, @muni_Codigo, @depa_Id, @muni_UsuCreacion, NULL, NULL);

				SELECT 1 codeStatus
			END
		ELSE 
			BEGIN
				UPDATE gral.tbMunicipios
				SET muni_Nombre = @muni_Nombre, 
					depa_Id = @depa_Id, 
					muni_UsuCreacion = @muni_UsuCreacion, 
					muni_FechaCreacion = GETDATE(), 
					muni_UsuModificacion = NULL, 
					muni_FechaModificacion = NULL, 
					muni_Estado = 1
				WHERE muni_Codigo = @muni_Codigo
				

				SELECT 1 codeStatus
			END
	END TRY
	BEGIN CATCH
				SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE  ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMunicipios_Update
(@muni_Id INT,
 @muni_Nombre NVARCHAR(100),
 @depa_Id INT,
 @muni_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE gral.tbMunicipios
		SET muni_Nombre = @muni_Nombre, 
			depa_Id = @depa_Id, 
			muni_UsuModificacion = @muni_UsuModificacion, 
			muni_FechaModificacion = GETDATE()
		WHERE muni_Id = @muni_Id
		
		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE  ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMunicipios_Delete
(@muni_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE gral.tbMunicipios
		SET muni_Estado = 0
		WHERE muni_Id = @muni_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT  0 codeStatus
	END CATCH
END

--**************  INDEX  ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMunicipios_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbMunicipios
	WHERE muni_Estado = 1;
END

--**************  FIND  ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMunicipios_Find 
(@muni_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbMunicipios
	WHERE muni_Id = @muni_Id;
END


--******************************************--
--************ TABLA METODO DE PAGO ************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbMetodosdePago
AS
SELECT	meto_Id, 
		meto_Descripcion, 
		meto_UsuCreacion, 
		t2.user_NombreUsuario AS user_Creacion,
		meto_FechaCreacion, 
		meto_UsuModificacion, 
		t3.user_NombreUsuario AS user_Modificacion,
		meto_FechaModificacion, 
		meto_Estado
FROM gral.tbMetodosdePago AS T1 INNER JOIN acce.tbUsuarios AS T2
ON T1.meto_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3
ON T1.meto_UsuCreacion = T3.[user_Id]

--**************  CREATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMetodosdePago_Insert
(@meto_Descripcion NVARCHAR(100),
 @meto_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY 
		INSERT INTO gral.tbMetodosdePago (meto_Descripcion, meto_UsuCreacion, meto_UsuModificacion, meto_FechaModificacion)
		VALUES (@meto_Descripcion, @meto_UsuCreacion, NULL, NULL);

		SELECT 1 AS codeStatu
	END TRY
	BEGIN CATCH
		SELECT 0 AS codeStatu
	END CATCH
END

--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMetodosdePago_Update
(@meto_Id INT,
 @meto_Descripcion NVARCHAR(100),
 @meto_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY	
		UPDATE gral.tbMetodosdePago
		SET meto_Descripcion = @meto_Descripcion, 
			meto_UsuModificacion = @meto_UsuModificacion, 
			meto_FechaModificacion = GETDATE()
		WHERE meto_Id = @meto_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMetodosdePago_Delete
(@meto_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE gral.tbMetodosdePago
		SET meto_Estado = 0
		WHERE meto_Id = @meto_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMetodosdePago_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbMetodosdePago
	WHERE meto_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbMetodosdePago_Find
(@meto_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbMetodosdePago
	WHERE meto_Id = @meto_Id;
END



--******************************************--
--************ TABLA CARGOS ************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbCargos
AS
SELECT	carg_Id, 
		carg_Descripcion, 
		carg_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		carg_FechaCreacion, 
		carg_UsuModificacion, 
		T3.user_NombreUsuario AS user_Modificacion,
		carg_FechaModificacion, 
		carg_Estado
FROM gral.tbCargos AS T1 INNER JOIN acce.tbUsuarios AS T2
ON T1.carg_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3
ON T1.carg_UsuModificacion = T3.[user_Id]

--**************  CREATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCargos_Insert
(@carg_Descripcion NVARCHAR(100),
 @carg_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY 
		INSERT INTO [gral].[tbCargos] (carg_Descripcion, carg_UsuCreacion, carg_UsuModificacion, carg_FechaModificacion)
		VALUES (@carg_Descripcion, @carg_UsuCreacion, NULL, NULL);

		SELECT 1 AS codeStatu
	END TRY
	BEGIN CATCH
		SELECT 0 AS codeStatu
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCargos_Update
(@carg_Id INT,
 @carg_Descripcion NVARCHAR(100),
 @carg_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE	gral.tbCargos
		SET		carg_Descripcion = @carg_Descripcion, 
				carg_UsuModificacion = @carg_UsuModificacion, 
				carg_FechaModificacion = GETDATE()
		WHERE	carg_Id = @carg_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCargos_Delete
(@carg_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE	gral.tbCargos
		SET		carg_Estado = 0
		WHERE	carg_Id = @carg_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCargos_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbCargos
	WHERE carg_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbCargos_Find 
(@carg_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbCargos
	WHERE carg_Id = @carg_Id;
END


--***********************************************--
--************ TABLA ESTADOS CIVILES ************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW gral.VW_tbEstadosCiviles
AS
SELECT	eciv_Id, 
		eciv_Descripcion,
		eciv_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		eciv_FechaCreacion, 
		eciv_UsuModificacion, 
		T3.user_NombreUsuario AS user_Modificacion,
		eciv_FechaModificacion, 
		eciv_Estado
FROM	[gral].[tbEstadosCiviles] AS T1 INNER JOIN acce.tbUsuarios AS T2
ON T1.eciv_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3
ON T1.eciv_UsuModificacion = T3.[user_Id]



--**************  CREATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbEstadosCiviles_Insert
(@eciv_Descripcion NVARCHAR(100),
 @eciv_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY 
		INSERT INTO [gral].[tbEstadosCiviles] (eciv_Descripcion, eciv_UsuCreacion, eciv_UsuModificacion, eciv_FechaModificacion)
		VALUES (@eciv_Descripcion, @eciv_UsuCreacion, NULL, NULL);

		SELECT 1 AS codeStatu
	END TRY
	BEGIN CATCH
		SELECT 0 AS codeStatu
	END CATCH
END

--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbEstadosCiviles_Update
(@eciv_Id INT,
 @eciv_Descripcion NVARCHAR(100),
 @eciv_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE	gral.tbEstadosCiviles
		SET		eciv_Descripcion = @eciv_Descripcion, 
				eciv_UsuModificacion = @eciv_UsuModificacion, 
				eciv_FechaModificacion = GETDATE()
		WHERE	eciv_Id = @eciv_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbEstadosCiviles_Delete
(@eciv_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE	gral.tbEstadosCiviles
		SET		eciv_Estado = 0
		WHERE	eciv_Id = @eciv_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbEstadosCiviles_Index
AS
BEGIN
	SELECT * FROM gral.VW_tbEstadosCiviles
	WHERE eciv_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE gral.UDP_tbEstadosCiviles_Find 
(@eciv_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbEstadosCiviles
	WHERE eciv_Id = @eciv_Id;
END



--***************** ACCE ***********************--
--***********************************************--


-- ************* LOGIN *****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_IniciarSesion
(@user_NombreUsuario	NVARCHAR(100),
 @user_Contrasena	NVARCHAR(MAX))
AS
BEGIN
	DECLARE @password NVARCHAR(MAX)=(SELECT HASHBYTES('Sha2_512', @user_Contrasena));
	SELECT *
	FROM acce.VW_tbUsuarios
	WHERE user_NombreUsuario = @user_NombreUsuario AND user_Contrasena = @password
END



GO
CREATE OR ALTER PROCEDURE acce.UDP_RecuperarUsuario
(@user_NombreUsuario	NVARCHAR(100),
 @user_Contrasena	NVARCHAR(MAX))
AS
BEGIN
	BEGIN TRY

		DECLARE @password NVARCHAR(MAX)=(SELECT HASHBYTES('Sha2_512', @user_Contrasena));

		UPDATE [acce].[tbUsuarios]
		   SET [user_Contrasena] = @password
		 WHERE @user_NombreUsuario = user_NombreUsuario

 
		 IF EXISTS (select * FROM acce.tbUsuarios WHERE user_NombreUsuario = @user_NombreUsuario AND [user_Contrasena] = @Password)
			 BEGIN
				SELECT 1 codeStatus
			 END
		 ELSE
			BEGIN
			SELECT 0 codeStatus
		  END
	END TRY
	BEGIN CATCH
	 SELECT 0 codeStatus
	END CATCH
END






-- ************* TABLA USUARIOS *****************--


--************  INSERT **************---
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbusuarios_INSERT
(@user_NombreUsuario NVARCHAR(100),
 @user_Contrasena NVARCHAR(MAX),
 @user_EsAdmin BIT,
 @role_Id INT,
 @empe_Id INT,
 @user_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT * FROM acce.tbUsuarios WHERE user_NombreUsuario = @user_NombreUsuario AND user_Estado = 1 )
			BEGIN
				SELECT 2 AS Proceso
			END

		ELSE IF NOT EXISTS (SELECT * FROM acce.tbUsuarios WHERE user_NombreUsuario = @user_NombreUsuario)
			BEGIN
				INSERT INTO [acce].[tbUsuarios] (user_NombreUsuario, user_Contrasena, user_EsAdmin, role_Id, empe_Id, user_UsuCreacion, user_UsuModificacion, user_FechaModificacion)
				VALUES (@user_NombreUsuario, HASHBYTES('SHA2_512',@user_Contrasena), @user_EsAdmin, @role_Id, @empe_Id, @user_UsuCreacion, NULL, NULL)
				SELECT 1 AS Proceso

				SELECT 1 AS Proceso
			END
		ELSE
			BEGIN
				UPDATE [acce].[tbUsuarios]
				SET [user_Estado] = 1,
					[user_UsuCreacion] = @user_UsuCreacion,
					[user_FechaCreacion] = GETDATE(),
					[user_UsuModificacion] = NULL,
					[user_FechaModificacion] = NULL
				WHERE [user_NombreUsuario] = @user_NombreUsuario;

				SELECT 1 AS Proceso;
			END

	END TRY
	BEGIN CATCH
		SELECT 0 AS Processo
	END CATCH
END

--*********** UPDATE  ****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbusuarios_UPDATE
(@user_Id INT,
 @user_EsAdmin BIT,
 @role_Id INT,
 @empe_Id INT,
 @user_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE [acce].[tbUsuarios]
		SET [user_EsAdmin] = @user_EsAdmin,
			[role_Id] = @role_Id,
			[empe_Id] = @empe_Id,
			[user_UsuModificacion] = @user_UsuModificacion,
			[user_FechaModificacion] = GETDATE()
		WHERE [user_Id] = @user_Id;
		SELECT 1 AS Proceso;

	END TRY
	BEGIN CATCH
		SELECT 0 AS Proceso;

	END CATCH
END


--********** DELETE ***********--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbusuarios_DELETE
(@user_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE [acce].[tbUsuarios]
		SET [user_Estado]  = 0,
			[user_FechaModificacion] = GETDATE()
		WHERE [user_Id] = @user_Id

		SELECT 1 AS Proceso

	END TRY
	BEGIN CATCH
		SELECT 0 AS Proceso
	END CATCH
END


--*********** VIEW ********************---
GO
CREATE OR ALTER VIEW acce.VW_tbUsuarios
AS
SELECT T1.[user_Id]
      ,T1.[user_NombreUsuario]
      ,T1.[user_Contrasena]
      ,T1.[user_EsAdmin]
      ,T1.[role_Id]
	  ,T4.role_Nombre
      ,T1.[empe_Id]
	  ,T5.empe_Nombres
	  ,T5.empe_Apellidos
	  ,T5.empe_Nombres + ' ' + T5.empe_Apellidos As empe_NombreCompleto
	  ,T5.sucu_Id
	  ,T7.sucu_Nombre
	  ,T5.carg_Id
	  ,T6.carg_Descripcion
      ,T1.[user_UsuCreacion]
      ,T1.[user_FechaCreacion]
      ,T1.[user_UsuModificacion]
      ,T1.[user_FechaModificacion]
      ,T1.[user_Estado]
  FROM [acce].[tbUsuarios] T1 LEFT JOIN acce.tbRoles T4
  ON T1.role_Id = T4.role_Id INNER JOIN pbli.tbEmpleados T5
  ON T1.empe_Id = T5.empe_Id INNER JOIN gral.tbCargos T6
  ON T5.carg_Id = T6.carg_Id INNER JOIN pbli.tbSucursales T7 
  ON T5.sucu_Id = T7.sucu_Id INNER JOIN acce.tbUsuarios T2
  ON T1.user_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios T3
  ON T1.user_UsuModificacion = T3.[user_Id]


--************* INDEX ***********--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbUsuarios_Index
AS
BEGIN
	SELECT * FROM acce.VW_tbUsuarios
	WHERE user_Estado = 1
END


--************** FIND *****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbUsuarios_Find
(@user_Id INT)
AS
BEGIN
	SELECT * FROM acce.VW_tbUsuarios
	WHERE [user_Id] = @user_Id;
END



--***********************************************--
-- ************* TABLA ROLES *****************--

GO
CREATE OR ALTER VIEW acce.VW_tbRoles
AS
SELECT T1.[role_Id]
      ,[role_Nombre]
      ,[role_UsuCreacion]
	  ,t2.user_NombreUsuario AS user_Creacion
      ,[role_FechaCreacion]
      ,[role_UsuModificacion]
	  ,t3.user_NombreUsuario AS user_Modificacion
      ,[role_FechaModificacion]
      ,[role_Estado]
  FROM [acce].[tbRoles] T1 INNER JOIN acce.tbUsuarios T2
  ON T1.role_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios T3
  ON T1.role_UsuModificacion = T3.[user_Id]

--************** INDEX *****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbRoles_Index
AS 
BEGIN
	SELECT * FROM acce.VW_tbRoles
	WHERE role_Estado = 1
END


--************** FIND *****************--

GO
CREATE OR ALTER PROCEDURE acce.UDP_tbRoles_Find
(@role_Id	INT)
AS 
BEGIN
	SELECT * FROM acce.VW_tbRoles
	WHERE role_Id = @role_Id
END



--************** INSERT *****************--
Go
CREATE OR ALTER PROCEDURE acce.UDP_tbRoles_Insert 
 (@role_Nombre NVARCHAR(100),
  @role_UsuCreacion INT)
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT * FROM acce.tbRoles WHERE role_Nombre = @role_Nombre AND role_Estado = 1)
        BEGIN

            SELECT 2 codeStatus

        END
        ELSE IF NOT EXISTS (SELECT * FROM acce.tbRoles WHERE role_Nombre = @role_Nombre)
        BEGIN
		INSERT INTO [acce].[tbRoles]
				   ([role_Nombre]
				   ,[role_UsuCreacion]
				   ,[role_FechaCreacion]
				   ,[role_UsuModificacion]
				   ,[role_FechaModificacion]
				   ,[role_Estado])
			 VALUES
				   (@role_Nombre
				   ,@role_UsuCreacion
				   ,GETDATE()
				   ,Null
				   ,Null
				   ,1)

            SELECT SCOPE_IDENTITY() 
			END
        ELSE
        BEGIN
            UPDATE acce.tbRoles
            SET  role_Estado = 1
				,role_Nombre = @role_Nombre
				,role_UsuCreacion = @role_UsuCreacion
				,role_FechaCreacion = GETDATE()
				,role_UsuModificacion = NULL
				,role_FechaModificacion = NULL
            WHERE role_Nombre = @role_Nombre

            select role_Id From acce.tbRoles  WHERE role_Nombre = @role_Nombre
        END

    END TRY
    BEGIN CATCH
        SELECT 0 codeStatus
    END CATCH
END
GO

--************** UPDATE *****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbRoles_Update
  (@role_Id				INT,
  @role_Nombre			NVARCHAR(100),
  @role_UsuModificacion INT)
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT * FROM acce.tbRoles WHERE (role_Nombre = @role_Nombre AND role_Id != @role_Id))
			BEGIN
				SELECT 2 codeStatus
			END
        ELSE
			BEGIN

				UPDATE acce.tbRoles
					SET  role_Estado = 1
						,role_Nombre = @role_Nombre
						,role_UsuModificacion = @role_UsuModificacion
						,role_FechaModificacion = GETDATE()
					WHERE role_Id = @role_Id


				SELECT 1 codeSatus
			END
    END TRY
    BEGIN CATCH
        SELECT 0 codeSatus
    END CATCH
END



--************** DELETE *****************--
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbRoles_Delete
  @role_Id INT
AS
BEGIN
	BEGIN TRY
		  IF EXISTS (SELECT * FROM acce.tbUsuarios WHERE (role_Id = @role_Id))
			BEGIN
				SELECT -1
			END
		ELSE
			BEGIN 
				UPDATE acce.tbRoles
				SET role_Estado = 0
				WHERE role_Id = @role_Id
		
				DELETE FROM [acce].[tbPantallasPorRoles]
				WHERE role_Id = @role_Id

				SELECT 1 codeStatus
			END
	
	END TRY
	BEGIN CATCH
		SELECT 0 codeStaus
	END CATCH
END


--***************** PBLI ***********************--
--***********************************************--
-- ************* TABLA EMPLEADOS *****************--


--************** VIEW *****************--
GO
CREATE OR ALTER VIEW pbli.VW_tbEmpleados
AS
SELECT T1.[empe_Id]
      ,[empe_Nombres]
      ,[empe_Apellidos]
	  ,[empe_Nombres] + ' ' +  [empe_Apellidos] AS empe_NombreCompleto
      ,[empe_Identidad]
      ,[empe_FechaNacimiento]
      ,[empe_Sexo]
      ,T1.[eciv_Id]
	  ,T4.eciv_Descripcion
      ,T1.[muni_Id]
	  ,T5.muni_Codigo
	  ,T5.muni_Nombre
	  ,T6.depa_Id
	  ,T6.depa_Codigo
	  ,T6.depa_Nombre
      ,[empe_DireccionExacta]
      ,[empe_Telefono]
      ,T7.[sucu_Id]
	  ,T7.sucu_Nombre
      ,T8.[carg_Id]
	  ,T8.carg_Descripcion
      ,[empe_UsuCreacion]
	  ,t2.user_NombreUsuario AS user_Creacion
      ,[empe_FechaCreacion]
      ,[empe_UsuModificacion]
	  ,t3.user_NombreUsuario AS user_Modificacion
      ,[empe_FechaModificacion]
      ,[empe_Estado]
  FROM [pbli].[tbEmpleados] T1 INNER JOIN gral.tbEstadosCiviles T4
  ON T4.eciv_Id = T1.eciv_Id INNER JOIN gral.tbMunicipios T5
  ON T5.muni_Id = T1.muni_Id INNER JOIN gral.tbDepartamentos T6
  ON T5.depa_Id = T6.depa_Id INNER JOIN pbli.tbSucursales T7
  ON T7.sucu_Id = T1.sucu_Id INNER JOIN gral.tbCargos T8
  ON T8.carg_Id	= T1.carg_Id INNER JOIN acce.tbUsuarios T2
  ON T1.empe_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios T3
  ON T1.empe_UsuModificacion = T3.[user_Id]



--************** INDEX *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbEmpleados_Index
AS 
BEGIN
	SELECT * FROM pbli.VW_tbEmpleados
	WHERE empe_Estado = 1
END


--************** FIND *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbEmpleados_Find
(@empe_Id	INT)
AS 
BEGIN
	SELECT * FROM pbli.VW_tbEmpleados
	WHERE empe_Id = @empe_Id
END


--************** INSERT *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbEmpleados_Insert 
(@empe_Nombres NVARCHAR(200),
 @empe_Apellidos NVARCHAR(200),
 @empe_Identidad NVARCHAR(15),
 @empe_FechaNacimiento DATE,
 @empe_Sexo char(1),
 @eciv_Id INT,
 @muni_Id INT,
 @empe_DireccionExacta NVARCHAR(250),
 @empe_Telefono NVARCHAR(20),
 @sucu_Id INT,
 @carg_Id INT,
 @empe_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
        
		INSERT INTO [pbli].[tbEmpleados]
				([empe_Nombres]
				,[empe_Apellidos]
				,[empe_Identidad]
				,[empe_FechaNacimiento]
				,[empe_Sexo]
				,[eciv_Id]
				,[muni_Id]
				,[empe_DireccionExacta]
				,[empe_Telefono]
				,[sucu_Id]
				,[carg_Id]
				,[empe_UsuCreacion]
				,[empe_FechaCreacion]
				,[empe_UsuModificacion]
				,[empe_FechaModificacion]
				,[empe_Estado])
			VALUES
				(@empe_Nombres
				,@empe_Apellidos
				,@empe_Identidad
				,@empe_FechaNacimiento
				,@empe_Sexo
				,@eciv_Id
				,@muni_Id
				,@empe_DireccionExacta
				,@empe_Telefono
				,@sucu_Id
				,@carg_Id
				,@empe_UsuCreacion
				,GETDATE()
				,NULL
				,NULL
				,1)

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--************** UPDATE *****************--
Go
CREATE OR ALTER PROCEDURE pbli.UDP_tbEmpleados_Update
(@empe_Id INT,
 @empe_Nombres NVARCHAR(200),
 @empe_Apellidos NVARCHAR(200),
 @empe_Identidad NVARCHAR(15),
 @empe_FechaNacimiento DATE,
 @empe_Sexo char(1),
 @eciv_Id INT,
 @muni_Id INT,
 @empe_DireccionExacta NVARCHAR(250),
 @empe_Telefono NVARCHAR(20),
 @sucu_Id INT,
 @carg_Id INT,
 @empe_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
      
		UPDATE [pbli].[tbEmpleados]
		SET [empe_Nombres] = @empe_Nombres
			,[empe_Apellidos] = @empe_Apellidos
			,[empe_Identidad] = @empe_Identidad
			,[empe_FechaNacimiento] = @empe_FechaNacimiento
			,[empe_Sexo] = @empe_Sexo
			,[eciv_Id] = @eciv_Id
			,[muni_Id] = @muni_Id
			,[empe_DireccionExacta] = @empe_DireccionExacta
			,[empe_Telefono] = @empe_Telefono
			,[sucu_Id] = @sucu_Id
			,[carg_Id] = @carg_Id
			,[empe_UsuModificacion] =  @empe_UsuModificacion
			,[empe_FechaModificacion] = GETDATE()
		WHERE empe_Id = @empe_Id

		SELECT 1 codeStatus

	END TRY
	BEGIN CATCH
		SELECT 0  codeStatus
	END CATCH
END


--************** DELETE *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbEmpleados_Delete
(@empe_Id INT)
AS
BEGIN
	BEGIN TRY
		
		UPDATE pbli.tbEmpleados
		SET empe_Estado = 0
		WHERE empe_Id = @empe_Id
		
		SELECT 1 codestatus
	
	END TRY
	BEGIN CATCH
		SELECT 0 codestatus
	END CATCH
END



--******************************************--
--*********** TABLA CLIENTES **************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW pbli.VW_tbClientes
AS
SELECT	clie_Id, 
		clie_Nombres, 
		clie_Apellidos, 
		clie_Identidad, 
		clie_FechaNacimiento, 
		clie_Sexo, 
		T1.eciv_Id, 
		T6.eciv_Descripcion,
		T1.muni_Id, 
		T4.muni_Codigo,
		T4.muni_Nombre,
		T5.depa_Id,
		T5.depa_Codigo,
		T5.depa_Nombre,
		clie_DireccionExacta, 
		clie_Telefono,
		clie_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		clie_FechaCreacion, 
		clie_UsuModificacion, 
		t3.user_NombreUsuario AS user_Modificacion,
		clie_FechaModificacion, 
		clie_Estado
FROM [pbli].[tbClientes] AS T1 INNER JOIN gral.tbMunicipios AS T4 
ON T1.muni_Id = T4.muni_Id INNER JOIN gral.tbDepartamentos AS T5
ON T4.depa_Id = T5.depa_Id INNER JOIN gral.tbEstadosCiviles AS T6
ON T1.eciv_Id = T6.eciv_Id INNER JOIN [acce].[tbUsuarios] AS T2
ON T1.clie_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.clie_UsuModificacion = T3.[user_Id];


--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbClientes_Insert
(
@clie_Nombres			NVARCHAR(200),
@clie_Apellidos			NVARCHAR(200), 
@clie_Identidad			VARCHAR(15), 
@clie_FechaNacimiento	DATE, 
@clie_Sexo				CHAR(1),
@eciv_Id				INT,
@muni_Id				INT, 
@clie_DireccionExacta	NVARCHAR(250),
@clie_Telefono			NVARCHAR(20),
@clie_UsuCreacion		INT
)
AS
BEGIN
	BEGIN TRY 
		IF EXISTS (SELECT * FROM pbli.tbClientes WHERE clie_Identidad = @clie_Identidad AND clie_Estado = 1)
			BEGIN
				SELECT 2 codeStatus
			END
		ELSE IF NOT EXISTS (SELECT * FROM pbli.tbClientes WHERE clie_Identidad = @clie_Identidad)
			BEGIN
				INSERT INTO [pbli].[tbClientes](clie_Nombres, clie_Apellidos, clie_Identidad, clie_FechaNacimiento, clie_Sexo, eciv_Id, muni_Id, clie_DireccionExacta, clie_Telefono, clie_UsuCreacion)
				VALUES	(@clie_Nombres, @clie_Apellidos, @clie_Identidad, @clie_FechaNacimiento, @clie_Sexo, @eciv_Id, @muni_Id, @clie_DireccionExacta, @clie_Telefono, @clie_UsuCreacion)

				SELECT 1 codeStatus
			END
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbClientes_Update
(
@clie_Id				INT,
@clie_Nombres			NVARCHAR(200),
@clie_Apellidos			NVARCHAR(200), 
@clie_Identidad			VARCHAR(15), 
@clie_FechaNacimiento	DATE, 
@clie_Sexo				CHAR(1),
@eciv_Id				INT,
@muni_Id				INT, 
@clie_DireccionExacta	NVARCHAR(250),
@clie_Telefono			NVARCHAR(20),
@clie_UsuModificacion	INT
)
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT * FROM pbli.tbClientes WHERE (clie_Identidad = @clie_Identidad AND clie_Id != @clie_Id))
			BEGIN
				SELECT 2 codeStatus
			END
		ELSE
			BEGIN
				UPDATE	pbli.tbClientes
				SET		clie_Nombres = @clie_Nombres, 
						clie_Apellidos = @clie_Apellidos, 
						clie_Identidad = @clie_Identidad, 
						clie_FechaNacimiento = @clie_FechaNacimiento, 
						clie_Sexo = @clie_Sexo, 
						eciv_Id = @eciv_Id, 
						muni_Id = @muni_Id, 
						clie_DireccionExacta = @clie_DireccionExacta, 
						clie_Telefono = @clie_Telefono, 
						clie_UsuModificacion = @clie_UsuModificacion, 
						clie_FechaModificacion = GETDATE()
				WHERE	clie_Id = clie_Id		

				SELECT 1 codeStatus
			END 
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbClientes_Delete
(
@clie_Id INT
)
AS
BEGIN
 BEGIN TRY
	UPDATE	pbli.tbClientes
	SET		clie_Estado = 0
	WHERE	clie_Id = @clie_Id

	SELECT 1 codeStatus
 END TRY
 BEGIN CATCH
	SELECT 0 codeStatus
 END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbClientes_Index
AS
BEGIN
	SELECT * FROM pbli.VW_tbClientes 
	WHERE clie_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbClientes_Find 
(
@clie_Id INT
)
AS
BEGIN
	SELECT * FROM pbli.VW_tbClientes
	WHERE clie_Id = @clie_Id
END



--******************************************--
--*********** TABLA FACTURAS **************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW pbli.VW_tbFacturas
AS
SELECT	fact_Id, 
		T1.clie_Id, 
		T6.clie_Nombres + ' ' + T6.clie_Apellidos AS clie_NombreCompleto,
		T1.empe_Id, 
		T4.empe_Nombres + ' ' + T4.empe_Apellidos AS empe_NombreCompleto,
		T4.sucu_Id,
		T5.sucu_Nombre,
		T1.meto_Id, 
		T7.meto_Descripcion,
		fact_FechaCompra, 
		fact_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		fact_FechaCreacion, 
		fact_UsuModificacion, 
		T3.user_NombreUsuario AS user_Modificacion,
		fact_FechaModificacion, 
		fact_Estado
FROM [pbli].[tbFacturas] AS T1 INNER JOIN pbli.tbEmpleados AS T4
ON T1.empe_Id = T4.empe_Id INNER JOIN pbli.tbSucursales AS T5 
ON T4.sucu_Id = T5.sucu_Id INNER JOIN pbli.tbClientes AS T6
ON T1.clie_Id = T6.clie_Id INNER JOIN gral.tbMetodosdePago AS T7
ON T1.meto_Id = T7.meto_Id INNER JOIN [acce].[tbUsuarios] AS T2
ON T1.fact_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.fact_UsuModificacion = T3.[user_Id];

--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbFacturas_Insert
(
@clie_Id			INT, 
@empe_Id			INT, 
@meto_Id			INT, 
@fact_UsuCreacion	INT
)
AS
BEGIN
	BEGIN TRY

		INSERT INTO [pbli].[tbFacturas](clie_Id, empe_Id, meto_Id, fact_FechaCompra, fact_UsuCreacion)
		VALUES	(@clie_Id, @empe_Id, @meto_Id, GETDATE(), @fact_UsuCreacion)
		SELECT 1 codeStatus

	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbFacturas_Update
(
@fact_Id				INT,
@clie_Id				INT, 
@empe_Id				INT, 
@meto_Id				INT, 
@fact_UsuModificacion	INT
)
AS
BEGIN
	BEGIN TRY
		UPDATE	pbli.tbFacturas
		SET		clie_Id = @clie_Id, 
				empe_Id = @empe_Id, 
				meto_Id = @meto_Id, 
				fact_UsuModificacion = @fact_UsuModificacion, 
				fact_FechaModificacion = GETDATE()
		WHERE	fact_Id = @fact_Id		

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbFacturas_Delete
(
@fact_Id INT
)
AS
BEGIN
 BEGIN TRY
	UPDATE	pbli.tbFacturas
	SET		fact_Estado = 0
	WHERE	fact_Id = @fact_Id

	UPDATE	pbli.tbFacturaDetalle
	SET		fdet_Estado = 0
	WHERE	fact_Id = @fact_Id

	SELECT 1 codeStatus
 END TRY
 BEGIN CATCH
	SELECT 0 codeStatus
 END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbFacturas_Index
AS
BEGIN
	SELECT * FROM pbli.VW_tbFacturas 
	WHERE fact_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbFacturas_Find 
(
@fact_Id INT
)
AS
BEGIN
	SELECT * FROM pbli.VW_tbFacturas
	WHERE fact_Id = @fact_Id
END




--******************************************--
--*********** TABLA INSUMOS **************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW pbli.VW_tbInsumos
AS
SELECT	insu_Id, 
		insu_Nombre, 
		T1.cate_Id, 
		T4.cate_Descripcion,
		insu_Precio, 
		T1.prov_Id, 
		T5.prov_Nombre,
		insu_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		insu_FechaCreacion, 
		insu_UsuModificacion,
		T3.user_NombreUsuario AS user_Modificacion,
		insu_FechaModificacion, 
		insu_Estado

FROM [pbli].[tbInsumos] AS T1 INNER JOIN gral.tbCategorias AS T4 
ON T1.cate_Id = T4.cate_Id INNER JOIN pbli.tbProveedores AS T5 
ON T1.prov_Id = T5.prov_Id INNER JOIN [acce].[tbUsuarios] AS T2
ON T1.insu_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.insu_UsuModificacion = T3.[user_Id];

--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbInsumos_Insert
(
@insu_Nombre		NVARCHAR(100), 
@cate_Id			INT, 
@insu_Precio		DECIMAL(18,2), 
@prov_Id			INT, 
@insu_UsuCreacion	INT
)
AS
BEGIN
	BEGIN TRY

		INSERT INTO [pbli].[tbInsumos](insu_Nombre, cate_Id, insu_Precio, prov_Id, insu_UsuCreacion)
		VALUES	(@insu_Nombre, @cate_Id, @insu_Precio, @prov_Id, @insu_UsuCreacion)
		SELECT 1 codeStatus

	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbInsumos_Update
(
@insu_Id				INT,
@insu_Nombre			NVARCHAR(100), 
@cate_Id				INT, 
@insu_Precio			DECIMAL(18,2), 
@prov_Id				INT, 
@insu_UsuModificacion	INT
)
AS
BEGIN
	BEGIN TRY
		UPDATE	pbli.tbInsumos
		SET		insu_Nombre = @insu_Nombre, 
				cate_Id = @cate_Id, 
				insu_Precio = @insu_Precio, 
				prov_Id = @prov_Id, 
				insu_UsuModificacion = @insu_UsuModificacion, 
				insu_FechaModificacion = GETDATE()
		WHERE	insu_Id = @insu_Id		

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbInsumos_Delete
(
@insu_Id INT
)
AS
BEGIN
 BEGIN TRY
	UPDATE	pbli.tbInsumos
	SET		insu_Estado = 0
	WHERE	insu_Id = @insu_Id

	SELECT 1 codeStatus
 END TRY
 BEGIN CATCH
	SELECT 0 codeStatus
 END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbInsumos_Index
AS
BEGIN
	SELECT * FROM pbli.VW_tbInsumos
	WHERE insu_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbInsumos_Find 
(
@insu_Id INT
)
AS
BEGIN
	SELECT * FROM pbli.VW_tbInsumos
	WHERE insu_Id = @insu_Id
END


--******************************************--
--*********** TABLA SUCURSALES **************--

--**************  VISTA ******************--
GO
CREATE OR ALTER VIEW pbli.VW_tbSucursales
AS
SELECT	sucu_Id, 
		sucu_Nombre, 
		T1.muni_Id, 
		T4.muni_Codigo,
		T4.muni_Nombre,
		T4.depa_Id,
		T5.depa_Codigo,
		T5.depa_Nombre,
		sucu_Direccion, 
		sucu_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		sucu_FechaCreacion, 
		sucu_UsuModificacion,
		T3.user_NombreUsuario AS user_Modificacion,
		sucu_FechaModificacion, 
		sucu_Estado

FROM [pbli].[tbSucursales] AS T1 INNER JOIN gral.tbMunicipios AS T4
ON T1.muni_Id = T4.muni_Id INNER JOIN gral.tbDepartamentos AS T5
ON T4.depa_Id = T5.depa_Id INNER JOIN [acce].[tbUsuarios] AS T2
ON T1.sucu_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3 
ON T1.sucu_UsuModificacion = T3.[user_Id];

--**************  INSERT ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbSucursales_Insert
(
@sucu_Nombre		NVARCHAR(200), 
@muni_Id			INT, 
@sucu_Direccion		NVARCHAR(200), 
@sucu_UsuCreacion	INT
)
AS
BEGIN
	BEGIN TRY

		INSERT INTO [pbli].[tbSucursales](sucu_Nombre, muni_Id, sucu_Direccion, sucu_UsuCreacion)
		VALUES	(@sucu_Nombre, @muni_Id, @sucu_Direccion, @sucu_UsuCreacion)
		SELECT 1 codeStatus

	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--**************  UPDATE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbSucursales_Update
(
@sucu_Id				INT,
@sucu_Nombre			NVARCHAR(200), 
@muni_Id				INT, 
@sucu_Direccion			NVARCHAR(200), 
@sucu_UsuCreacion		INT,
@sucu_UsuModificacion	INT
)
AS
BEGIN
	BEGIN TRY
		UPDATE	pbli.tbSucursales
		SET		sucu_Nombre = @sucu_Nombre, 
				muni_Id = @muni_Id, 
				sucu_Direccion = @sucu_Direccion, 
				sucu_UsuModificacion = @sucu_UsuModificacion, 
				sucu_FechaModificacion = GETDATE()
		WHERE	sucu_Id = @sucu_Id		

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--**************  DELETE ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbSucursales_Delete
(
@sucu_Id INT
)
AS
BEGIN
 BEGIN TRY
	UPDATE	pbli.tbSucursales
	SET		sucu_Estado = 0
	WHERE	sucu_Id = @sucu_Id

	SELECT 1 codeStatus
 END TRY
 BEGIN CATCH
	SELECT 0 codeStatus
 END CATCH
END

--**************  INDEX ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbSucursales_Index
AS
BEGIN
	SELECT * FROM pbli.VW_tbSucursales
	WHERE sucu_Estado = 1;
END

--**************  FIND ******************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbSucursales_Find 
(
@sucu_Id INT
)
AS
BEGIN
	SELECT * FROM pbli.VW_tbSucursales
	WHERE sucu_Id = @sucu_Id
END


--***********************************************--
-- ************* TABLA SERVICIOS *****************--


--************** VIEW *****************--
GO
CREATE OR ALTER VIEW pbli.VW_tbservicios
AS
SELECT	serv_Id, 
		serv_Nombre, 
		serv_Precio, 
		serv_UsuCreacion, 
		T2.user_NombreUsuario AS user_Creacion,
		serv_FechaCreacion, 
		serv_UsuModificacion,
		T3.user_NombreUsuario AS user_Modificacion,
		serv_FechaModificacion, 
		serv_Estado
FROM [pbli].[tbServicios] AS T1 INNER JOIN acce.tbUsuarios AS T2
ON T1.serv_UsuCreacion = T2.[user_Id] LEFT JOIN acce.tbUsuarios AS T3
ON T1.serv_UsuModificacion = T3.[user_Id]


--************** INDEX *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbServicios_Index
AS
BEGIN
	SELECT * FROM [pbli].VW_tbservicios
	WHERE serv_Estado = 1;
END

--************** FIND *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbServicios_Find
(@serv_Id INT)
AS
BEGIN
	SELECT * FROM [pbli].VW_tbservicios
	WHERE serv_Id = @serv_Id;
END

--************** INSERT *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbServicios_Create
(@serv_Nombre NVARCHAR(200),
 @serv_Precio DECIMAL(18,2),
 @serv_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
		INSERT INTO [pbli].[tbServicios]
				   ([serv_Nombre]
				   ,[serv_Precio]
				   ,[serv_UsuCreacion]
				   ,[serv_UsuModificacion]
				   ,[serv_FechaModificacion])
			 VALUES
				   (@serv_Nombre
				   ,@serv_Precio
				   ,@serv_UsuCreacion 
				   ,NULL
				   ,NULL);
		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END


--************** UPDATE *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbServicios_Update
(@serv_Id INT,
 @serv_Nombre NVARCHAR(200),
 @serv_Precio DECIMAL(18,2),
 @serv_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE pbli.tbServicios
		SET [serv_Nombre] = @serv_Nombre,
			[serv_Precio] = @serv_Precio,
			[serv_UsuModificacion] = @serv_UsuModificacion,
			[serv_FechaModificacion] = GETDATE()
		WHERE serv_Id = @serv_Id;

		SELECT 1 codeSatatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeSatatus
	END CATCH
END

--************** DELETE *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbServicios_Delete
(@serv_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE pbli.tbServicios
		SET [serv_Estado] = 0
		WHERE serv_Id = @serv_Id;

		SELECT 1 codeSatatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeSatatus
	END CATCH
END 


--**************************************************--
-- ************* TABLA PROVVEDORES *****************--

--************** VIEW *****************--
GO
CREATE OR ALTER VIEW pbli.VW_tbProveedores
AS
SELECT	prov_Id, 
		prov_Nombre, 
		T1.muni_Id, 
		T2.muni_Codigo,
		T2.muni_Nombre,
		T3.depa_Id,
		T3.depa_Codigo,
		T3.depa_Nombre,
		prov_Direccion, 
		prov_Correo, 
		prov_UsuCreacion, 
		T4.user_NombreUsuario AS user_Creacion,
		prov_FechaCreacion, 
		prov_UsuModificacion, 
		T5.user_NombreUsuario AS  user_Modificacion,
		prov_FechaModificacion, 
		prov_Estado
FROM	[pbli].[tbProveedores] AS T1 INNER JOIN gral.tbMunicipios AS T2
ON T1.muni_Id = T2.muni_Id INNER JOIN gral.tbDepartamentos AS T3
ON T2.depa_Id = T3.depa_Id INNER JOIN acce.tbUsuarios AS T4
ON T1.prov_UsuCreacion = T4.[user_Id] LEFT JOIN  acce.tbUsuarios AS T5
ON T1.prov_UsuModificacion = T5.[user_Id]

--************** INDEX *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbProveedores_Index
AS
BEGIN
	SELECT * FROM [pbli].VW_tbProveedores
	WHERE prov_Estado = 1;
END

--************** FIND *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbProveedores_Find
(@prov_Id INT)
AS
BEGIN
	SELECT * FROM [pbli].VW_tbProveedores
	WHERE prov_Id = @prov_Id;
END

--************** INSERT *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbProveedores_Insert
(@prov_Nombre NVARCHAR(100),
 @muni_Id	INT,
 @prov_Direccion NVARCHAR(300),
 @prov_Correo NVARCHAR(300),
 @prov_UsuCreacion INT)
AS
BEGIN
	BEGIN TRY
		INSERT INTO [pbli].[tbProveedores] (prov_Nombre, muni_Id, prov_Direccion, prov_Correo, prov_UsuCreacion, prov_UsuModificacion, prov_FechaModificacion)
		VALUES (@prov_Nombre,@muni_Id,@prov_Direccion,@prov_Correo,@prov_UsuCreacion, NULL, NULL);

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH 
		SELECT 0 codeStatus
	END CATCH
END

--************** UPDATE *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbProveedores_Update
(@prov_Id INT,
 @prov_Nombre NVARCHAR(100),
 @muni_Id	INT,
 @prov_Direccion NVARCHAR(300),
 @prov_Correo NVARCHAR(300),
 @prov_UsuModificacion INT)
AS
BEGIN
	BEGIN TRY
		UPDATE [pbli].[tbProveedores]
		SET prov_Nombre = @prov_Nombre, 
			muni_Id = @muni_Id, 
			prov_Direccion =  @prov_Direccion, 
			prov_Correo = @prov_Correo, 
			prov_UsuModificacion = @prov_UsuModificacion, 
			prov_FechaModificacion = GETDATE()
		WHERE prov_Id = @prov_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END

--************** DELETE *****************--
GO
CREATE OR ALTER PROCEDURE pbli.UDP_tbProveedores_Delete
(@prov_Id INT)
AS
BEGIN
	BEGIN TRY
		UPDATE [pbli].[tbProveedores]
		SET prov_Estado = 0
		WHERE prov_Id = @prov_Id

		SELECT 1 codeStatus
	END TRY
	BEGIN CATCH
		SELECT 0 codeStatus
	END CATCH
END



----/***********************************************\-----
--- ********** PROCIDIMIENTOS ADICIONALES **********---
GO
CREATE OR ALTER PROCEDURE gral.tbMunicipios_DDL 
(@depa_Id INT)
AS
BEGIN
	SELECT * FROM gral.VW_tbMunicipios
	WHERE depa_Id = @depa_Id
END