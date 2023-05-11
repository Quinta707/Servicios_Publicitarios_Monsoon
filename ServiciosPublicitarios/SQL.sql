CREATE DATABASE ServiciosPublicitarios

GO
USE ServiciosPublicitarios
GO 
CREATE SCHEMA gral;
GO
CREATE SCHEMA pbli;
GO
CREATE SCHEMA acce;
GO


--****************************************--
--****************************************--
-------------- ACCE  ------------- 


CREATE TABLE acce.tbRoles(
	role_Id					INT IDENTITY,
	role_Nombre				NVARCHAR(100) UNIQUE NOT NULL,
	role_UsuCreacion		INT NOT NULL,
	role_FechaCreacion		DATETIME NOT NULL CONSTRAINT DF_role_FechaCreacion DEFAULT(GETDATE()),
	role_UsuModificacion	INT,
	role_FechaModificacion	DATETIME,
	role_Estado				BIT NOT NULL CONSTRAINT DF_role_Estado DEFAULT(1)
	CONSTRAINT PK_acce_tbRoles_role_Id PRIMARY KEY(role_Id)
);
GO



CREATE TABLE acce.tbPantallas(
	pant_Id					INT IDENTITY,
	pant_Nombre				NVARCHAR(100) NOT NULL,
	pant_Url				NVARCHAR(300) NOT NULL,
	pant_Menu				NVARCHAR(300) NOT NULL,
	pant_HtmlId				NVARCHAR(80) NOT NULL,
	pant_UsuCreacion		INT NOT NULL,
	pant_FechaCreacion		DATETIME NOT NULL CONSTRAINT DF_pant_FechaCreacion DEFAULT(GETDATE()),
	pant_UsuModificacion	INT,
	pant_FechaModificacion	DATETIME,
	pant_Estado				BIT NOT NULL CONSTRAINT DF_pant_Estado DEFAULT(1)
	CONSTRAINT PK_acce_tbPantallas_pant_Id PRIMARY KEY(pant_Id)
);


CREATE TABLE acce.tbPantallasPorRoles(
	prol_Id						INT IDENTITY,
	role_Id						INT NOT NULL,
	pant_Id						INT NOT NULL,
	prol_UsuCreacion			INT NOT NULL,
	prol_FechaCreacion			DATETIME NOT NULL CONSTRAINT DF_pantrole_FechaCreacion DEFAULT(GETDATE()),
	prol_UsuModificacion		INT,
	prol_FechaModificacion		DATETIME,
	prol_Estado					BIT NOT NULL CONSTRAINT DF_pantrole_Estado DEFAULT(1)
	CONSTRAINT FK_acce_tbPantallasPorRoles_acce_tbRoles_role_Id FOREIGN KEY(role_Id) REFERENCES acce.tbRoles(role_Id),
	CONSTRAINT FK_acce_tbPantallasPorRoles_acce_tbPantallas_pant_Id FOREIGN KEY(pant_Id)	REFERENCES acce.tbPantallas(pant_Id),
	CONSTRAINT PK_acce_tbPantallasPorRoles_pantrole_Id PRIMARY KEY(prol_Id)
);
GO

CREATE TABLE acce.tbUsuarios(
	user_Id 				INT IDENTITY(1,1),
	user_NombreUsuario		NVARCHAR(100) NOT NULL,
	user_Contrasena			NVARCHAR(MAX) NOT NULL,
	user_EsAdmin			BIT,
	role_Id					INT,
	empe_Id					INT,
	user_UsuCreacion		INT NOT NULL,
	user_FechaCreacion		DATETIME NOT NULL CONSTRAINT DF_user_FechaCreacion DEFAULT(GETDATE()),
	user_UsuModificacion	INT,
	user_FechaModificacion	DATETIME,
	user_Estado				BIT NOT NULL CONSTRAINT DF_user_Estado DEFAULT(1)
	CONSTRAINT PK_acce_tbUsuarios_user_Id  PRIMARY KEY(user_Id),
);

GO
CREATE OR ALTER PROCEDURE acce.UDP_InsertUsuario
	@user_NombreUsuario NVARCHAR(100),	
    @user_Contrasena NVARCHAR(200),
	@user_EsAdmin BIT,					
    @role_Id INT, 
	@empe_Id INT										
AS
BEGIN
	DECLARE @password NVARCHAR(MAX)=(SELECT HASHBYTES('Sha2_512', @user_Contrasena));

	INSERT acce.tbUsuarios(user_NombreUsuario, user_Contrasena, user_EsAdmin, role_Id, empe_Id, user_UsuCreacion)
	VALUES(@user_NombreUsuario, @password, @user_EsAdmin, @role_Id, @empe_Id, 1);
END;


GO
EXEC acce.UDP_InsertUsuario 'Cristian', '123', 1, NULL, 1;
GO
ALTER TABLE acce.tbRoles
ADD CONSTRAINT FK_acce_tbRoles_acce_tbUsuarios_role_UsuCreacion_user_Id 	FOREIGN KEY(role_UsuCreacion) REFERENCES acce.tbUsuarios(user_Id),
	CONSTRAINT FK_acce_tbRoles_acce_tbUsuarios_role_UsuModificacion_user_Id FOREIGN KEY(role_UsuModificacion) REFERENCES acce.tbUsuarios(user_Id);

GO
INSERT INTO acce.tbRoles(role_Nombre, role_UsuCreacion)
VALUES ('Admin', 1)

GO
ALTER TABLE [acce].[tbUsuarios]
ADD CONSTRAINT FK_acce_tbUsuarios_acce_tbUsuarios_user_UsuCreacion_user_Id  FOREIGN KEY(user_UsuCreacion) REFERENCES acce.tbUsuarios([user_Id]),
	CONSTRAINT FK_acce_tbUsuarios_acce_tbUsuarios_user_UsuModificacion_user_Id  FOREIGN KEY(user_UsuModificacion) REFERENCES acce.tbUsuarios([user_Id]),
	CONSTRAINT FK_acce_tbUsuarios_acce_tbRoles_role_Id FOREIGN KEY(role_Id) REFERENCES acce.tbRoles(role_Id)

GO 
ALTER TABLE [acce].[tbPantallasPorRoles]
ADD CONSTRAINT FK_acce_tbPantallasPorRoles_acce_tbUsuarios_pantrole_UsuCreacion_user_Id FOREIGN KEY([prol_UsuCreacion]) REFERENCES acce.tbUsuarios([user_Id]),
	CONSTRAINT FK_acce_tbPantallasPorRoles_acce_tbUsuarios_pantrole_UsuModificacion_user_Id FOREIGN KEY([prol_UsuModificacion]) REFERENCES acce.tbUsuarios([user_Id])




--****************************************--
--****************************************--
------------------- GRAL  ------------------ 




--********TABLA DEPARTAMENTO****************---
GO
CREATE TABLE [gral].[tbDepartamentos](
    depa_Id                     INT IDENTITY(1,1),
	depa_Nombre 				NVARCHAR(100) NOT NULL,
	depa_Codigo  				CHAR(2) NOT NULL,
	depa_UsuCreacion			INT NOT NULL,
	depa_FechaCreacion			DATETIME NOT NULL CONSTRAINT DF_depa_FechaCreacion DEFAULT(GETDATE()),
	depa_UsuModificacion		INT,
	depa_FechaModificacion		DATETIME,
	depa_Estado					BIT NOT NULL CONSTRAINT DF_depa_Estado DEFAULT(1)
	CONSTRAINT PK_gral_tbDepartamentos_depa_Id 									PRIMARY KEY(depa_Id),
	CONSTRAINT FK_gral_tbDepartamentos_acce_tbUsuarios_depa_UsuCreacion_user_Id  		FOREIGN KEY(depa_UsuCreacion) 		REFERENCES acce.tbUsuarios(user_Id),
	CONSTRAINT FK_gral_tbDepartamentos_acce_tbUsuarios_depa_UsuModificacion_user_Id  	FOREIGN KEY(depa_UsuModificacion) 	REFERENCES acce.tbUsuarios(user_Id)
);


--********TABLA MUNICIPIO****************---
GO
CREATE TABLE gral.tbMunicipios(
	muni_Id                 INT IDENTITY(1,1),
    muni_Nombre				NVARCHAR(80) NOT NULL,
	muni_Codigo				CHAR(4)	NOT NULL,
	depa_Id					INT	NOT NULL,
	muni_UsuCreacion		INT	NOT NULL,
	muni_FechaCreacion		DATETIME NOT NULL CONSTRAINT DF_muni_FechaCreacion DEFAULT(GETDATE()),
	muni_UsuModificacion	INT,
	muni_FechaModificacion	DATETIME,
	muni_Estado				BIT	NOT NULL CONSTRAINT DF_muni_Estado DEFAULT(1)
	CONSTRAINT PK_gral_tbMunicipios_muni_Id 										PRIMARY KEY(muni_Id),
	CONSTRAINT FK_gral_tbMunicipios_gral_tbDepartamentos_depa_Id 					FOREIGN KEY (depa_Id) 						REFERENCES gral.tbDepartamentos(depa_Id),
	CONSTRAINT FK_gral_tbMunicipios_acce_tbUsuarios_muni_UsuCreacion_user_Id  		FOREIGN KEY(muni_UsuCreacion) 				REFERENCES acce.tbUsuarios(user_Id),
	CONSTRAINT FK_gral_tbMunicipios_acce_tbUsuarios_muni_UsuModificacion_user_Id  	FOREIGN KEY(muni_UsuModificacion) 			REFERENCES acce.tbUsuarios(user_Id)
);


--********* ESTADOS CIVILES ***************--
GO
CREATE TABLE gral.tbEstadosCiviles(
eciv_Id							INT IdENTITY(1,1),
eciv_Descripcion				VARCHAR(100),
eciv_UsuCreacion				INT NOT NULL,
eciv_FechaCreacion				DATETIME NOT NULL CONSTRAINT DF_gral_TbEstadosCiviles_eciv_FechaCreacion    DEFAULT(GETDATE()),
eciv_UsuModificacion			INT,
eciv_FechaModificacion			DATETIME,
eciv_Estado						BIT NOT NULL CONSTRAINT DF_gral_TbEstadosCiviles_eciv_Estado    DEFAULT(1)
CONSTRAINT     PK_gral_tbEstadosCiviles_ectv_Id PRIMARY KEY(eciv_Id),
CONSTRAINT     FK_gral_tbEstadosCiviles_UsuCreacion_usua_Id        FOREIGN KEY(eciv_UsuCreacion) REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT     FK_gral_tbEstadosCiviles_UsuModificacion_usua_Id    FOREIGN KEY(eciv_UsuModificacion) REFERENCES acce.tbUsuarios(user_Id)
);


--********** CARGOS ************--
GO
CREATE TABLE gral.tbCargos(
carg_Id INT IDENTITY(1,1),
carg_Descripcion			NVARCHAR(100) NOT NULL,
carg_UsuCreacion			INT NOT NULL,
carg_FechaCreacion			DATETIME CONSTRAINT DF_gral_tbCargos_carg_FechaCreacion DEFAULT(GETDATE()),
carg_UsuModificacion		INT ,
carg_FechaModificacion		DATETIME,
carg_Estado					BIT CONSTRAINT DF_gral_tbCargos_carg_Estado DEFAULT(1)
CONSTRAINT PK_gral_tbcargos_carg_Id                                  PRIMARY KEY(carg_Id),
CONSTRAINT PK_gral_tbCargos_acce_tbUsuarios_carg_UsuCreacion         FOREIGN KEY(carg_UsuCreacion) REFERENCES acce.tbUsuarios(User_Id),
CONSTRAINT PK_gral_tbCargos_acce_tbUsuarios_carg_UsuModificacion     FOREIGN KEY(carg_UsuModificacion) REFERENCES acce.tbUsuarios(User_Id)
);


--********** CATEGORIA ************--
GO
CREATE TABLE gral.tbCategorias(
cate_Id						INT IDENTITY(1,1),
cate_Descripcion			NVARCHAR(100),
cate_UsuCreacion			INT NOT NULL,
cate_FechaCreacion			DATETIME CONSTRAINT DF_gral_tbCategorias_cate_FechaCreacion DEFAULT(GETDATE()),
cate_UsuModificacion		INT ,
cate_FechaModificacion		DATETIME,
cate_Estado					BIT CONSTRAINT DF_gral_tbCategorias_cate_Estado DEFAULT(1)
CONSTRAINT PK_gral_tbCategorias_cate_Id                                  PRIMARY KEY(cate_Id ),
CONSTRAINT PK_gral_tbCategorias_acce_tbUsuarios_cate_UsuCreacion         FOREIGN KEY(cate_UsuCreacion) REFERENCES acce.tbUsuarios(User_Id),
CONSTRAINT PK_gral_tbCategorias_acce_tbUsuarios_cate_UsuModificacion     FOREIGN KEY(cate_UsuModificacion) REFERENCES acce.tbUsuarios(User_Id)
);



--********** METODOS DE PAGO ************--
GO 
CREATE TABLE gral.tbMetodosdePago(
meto_Id						INT IDENTITY(1,1),
meto_Descripcion			NVARCHAR(100),
meto_UsuCreacion			INT NOT NULL,
meto_FechaCreacion			DATETIME CONSTRAINT DF_gral_tbMetodosdePago_meto_FechaCreacion DEFAULT(GETDATE()),
meto_UsuModificacion		INT ,
meto_FechaModificacion		DATETIME,
meto_Estado					BIT CONSTRAINT DF_gral_tbMetodosdePago_meto_Estado DEFAULT(1)
CONSTRAINT PK_gral_tbMetodosdePago_meto_Id                                  PRIMARY KEY(meto_Id ),
CONSTRAINT PK_gral_tbMetodosdePago_acce_tbUsuarios_meto_UsuCreacion         FOREIGN KEY(meto_UsuCreacion) REFERENCES acce.tbUsuarios(User_Id),
CONSTRAINT PK_gral_tbMetodosdePago_acce_tbUsuarios_meto_UsuModificacion     FOREIGN KEY(meto_UsuModificacion) REFERENCES acce.tbUsuarios(User_Id)
);																						




--****************************************--
--****************************************--
------------------- PBLI  ------------------ 


--********TABLA SUCURSALES****************---
GO
CREATE TABLE pbli.tbSucursales(
sucu_Id							INT IDENTITY(1,1),
sucu_Nombre						NVARCHAR(200)   NOT NULL,
muni_Id							INT				NOT NULL,
sucu_Direccion					NVARCHAR(200)   NOT NULL,
sucu_UsuCreacion				INT             NOT NULL,
sucu_FechaCreacion				DATETIME         CONSTRAINT DF_pbli_tbSucursales_sucu_FechaCreacion    DEFAULT(GETDATE()),
sucu_UsuModificacion			INT,
sucu_FechaModificacion			DATETIME,
sucu_Estado						BIT             CONSTRAINT DF_pbli_tbSucursales_sucu_Estado DEFAULT (1)
CONSTRAINT PK_pbli_tbSucursales_sucu_Id                                  PRIMARY KEY(sucu_Id),
CONSTRAINT FK_pbli_tbSucursales_gral_tbMunicipios_muni_Id                FOREIGN KEY(muni_Id)                 REFERENCES gral.tbMunicipios(muni_Id),
CONSTRAINT FK_pbli_tbSucursales_acce_tbUsuarios_sucu_UsuCreacion         FOREIGN KEY(sucu_UsuCreacion)         REFERENCES acce.tbUsuarios(User_Id),
CONSTRAINT FK_pbli_tbSucursales_acce_tbUsuarios_sucu_UsuModificacion     FOREIGN KEY(sucu_UsuModificacion)     REFERENCES acce.tbUsuarios(User_Id)
);



--********TABLA EMPLEADOS****************---
GO
CREATE TABLE pbli.tbEmpleados(
empe_Id						INT IDENTITY(1,1),
empe_Nombres				NVARCHAR(200)	NOT NULL,
empe_Apellidos				NVARCHAR(200)	NOT NULL,
empe_Identidad				NVARCHAR(15)	NOT NULL,
empe_FechaNacimiento		DATE			NOT NULL,
empe_Sexo					CHAR(1)			NOT NULL,
eciv_Id					    INT				NOT NULL,
muni_Id						INT	    		NOT NULL,
empe_DireccionExacta		NVARCHAR(250)	NOT NULL,
empe_Telefono				NVARCHAR(20)	NOT NULL,
sucu_Id						INT				NOT NULL,
carg_Id						INT				NOT NULL,
empe_UsuCreacion			INT				NOT NULL,
empe_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbEmpleados_empe_FechaCreacion DEFAULT(GETDATE()),
empe_UsuModificacion		INT,
empe_FechaModificacion		DATETIME,
empe_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbEmpleados_empe_Estado DEFAULT(1),
	
CONSTRAINT PK_pbli_tbEmpleados_empe_Id 										PRIMARY KEY(empe_Id),
CONSTRAINT CK_pbli_tbEmpleados_empe_Sexo									CHECK(empe_sexo IN ('F', 'M')),
CONSTRAINT FK_pbli_tbEmpleados_gral_tbEstadosCiviles_eciv_Id        		FOREIGN KEY(eciv_Id)					    REFERENCES gral.tbEstadosCiviles(eciv_Id),			
CONSTRAINT FK_pbli_tbEmpleados_gral_tbMunicipios_muni_Id					FOREIGN KEY(muni_Id)						REFERENCES gral.tbMunicipios(muni_Id),
CONSTRAINT FK_pbli_tbEmpleados_gral_tbCargos_carg_Id						FOREIGN KEY(carg_Id)						REFERENCES gral.tbCargos(carg_Id),
CONSTRAINT FK_pbli_tbEmpleados_acce_tbUsuarios_UserCreate					FOREIGN KEY(empe_UsuCreacion)				REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbEmpleados_acce_tbUsuarios_UserUpdate					FOREIGN KEY(empe_UsuModificacion)			REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbEmpleados_pbli_tbSucursales_sucu_Id					FOREIGN KEY(sucu_Id)						REFERENCES pbli.tbSucursales(sucu_Id)		
);





--********TABLA CLIENTES****************---
GO
CREATE TABLE pbli.tbClientes(
clie_Id						INT IDENTITY(1,1),
clie_Nombres				NVARCHAR(200)	NOT NULL,
clie_Apellidos				NVARCHAR(200)	NOT NULL,
clie_Identidad				NVARCHAR(15)		NOT NULL,
clie_FechaNacimiento		DATE			NOT NULL,
clie_Sexo					CHAR(1)			NOT NULL,
eciv_Id					    INT				NOT NULL,
muni_Id						INT	    		NOT NULL,
clie_DireccionExacta		NVARCHAR(250)	NOT NULL,
clie_Telefono				NVARCHAR(20)	NOT NULL,
clie_UsuCreacion			INT				NOT NULL,
clie_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbClientes_clie_FechaCreacion DEFAULT(GETDATE()),
clie_UsuModificacion		INT,
clie_FechaModificacion		DATETIME,
clie_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbClientes_clie_Estado DEFAULT(1),
	
CONSTRAINT PK_pbli_tbClientes_empe_Id 										PRIMARY KEY(clie_Id),
CONSTRAINT CK_pbli_tbClientes_empe_Sexo										CHECK(clie_sexo IN ('F', 'M')),
CONSTRAINT FK_pbli_tbClientes_gral_tbEstadosCiviles_eciv_Id        			FOREIGN KEY(eciv_Id)					    REFERENCES gral.tbEstadosCiviles(eciv_Id),			
CONSTRAINT FK_pbli_tbClientes_gral_tbMunicipios_muni_Id						FOREIGN KEY(muni_Id)						REFERENCES gral.tbMunicipios(muni_Id),
CONSTRAINT FK_pbli_tbClientes_acce_tbUsuarios_UserCreate					FOREIGN KEY(clie_UsuCreacion)				REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbClientes_acce_tbUsuarios_UserUpdate					FOREIGN KEY(clie_UsuModificacion)			REFERENCES acce.tbUsuarios(user_Id),	
);				  



--********TABLA PROVEEDORES****************---
GO
CREATE TABLE pbli.tbProveedores(
prov_Id						INT IDENTITY(1,1),
prov_Nombre					NVARCHAR(150),
muni_Id						INT,
prov_Direccion				NVARCHAR(250),
prov_Correo					NVARCHAR(250),
prov_UsuCreacion			INT				NOT NULL,
prov_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbProveedores_prov_FechaCreacion DEFAULT(GETDATE()),
prov_UsuModificacion		INT,
prov_FechaModificacion		DATETIME,
prov_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbProveedores_prov_Estado DEFAULT(1),
CONSTRAINT PK_pbli_tbProveedores_prov_Id 									PRIMARY KEY(prov_Id),
CONSTRAINT FK_pbli_tbProveedores_gral_tbMunicipios_muni_Id					FOREIGN KEY(muni_Id)						REFERENCES gral.tbMunicipios(muni_Id),
CONSTRAINT FK_pbli_tbProveedores_acce_tbUsuarios_prov_UsuCreacion			FOREIGN KEY(prov_UsuCreacion)				REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbProveedores_acce_tbUsuarios_prov_UsuModificacion		FOREIGN KEY(prov_UsuModificacion)			REFERENCES acce.tbUsuarios(user_Id),	
);


--********TABLA INSUMOS	****************---
GO
CREATE TABLE pbli.tbInsumos(
insu_Id						INT IDENTITY(1,1),
insu_Nombre					NVARCHAR(100),
cate_Id						INT,
insu_Precio					DECIMAL(18,2),
prov_Id						INT,
insu_UsuCreacion			INT				NOT NULL,
insu_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbInsumos_insu_FechaCreacion DEFAULT(GETDATE()),
insu_UsuModificacion		INT,
insu_FechaModificacion		DATETIME,
insu_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbInsumos_insu_Estado DEFAULT(1),
CONSTRAINT PK_pbli_tbInsumos_insu_Id 								PRIMARY KEY(insu_Id),
CONSTRAINT FK_pbli_tbInsumos_gral_tbCategorias_cate_Id				FOREIGN KEY(cate_Id)				REFERENCES gral.tbCategorias(cate_Id),
CONSTRAINT FK_pbli_tbInsumos_pbli_tbProveedores_prov_Id				FOREIGN KEY(prov_Id)				REFERENCES pbli.tbProveedores(prov_Id),
CONSTRAINT FK_pbli_tbInsumos_acce_tbUsuarios_insu_UsuCreacion		FOREIGN KEY(insu_UsuCreacion)		REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbInsumos_acce_tbUsuarios_insu_UsuModificacion	FOREIGN KEY(insu_UsuModificacion)	REFERENCES acce.tbUsuarios(user_Id),	
);


--********TABLA SERVICIOS ****************---
GO
CREATE TABLE pbli.tbServicios(
serv_Id						INT IDENTITY(1,1),
serv_Nombre					NVARCHAR(150),
serv_Precio					DECIMAL(18,2),
serv_UsuCreacion			INT				NOT NULL,
serv_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbServicios_serv_FechaCreacion DEFAULT(GETDATE()),
serv_UsuModificacion		INT,
serv_FechaModificacion		DATETIME,
serv_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbServicios_serv_Estado DEFAULT(1)
CONSTRAINT PK_pbli_tbServecios_serv_Id		PRIMARY KEY(serv_Id),
CONSTRAINT FK_pbli_tbServicios_acce_tbUsuarios_serv_UsuCreacion		FOREIGN KEY(serv_UsuCreacion)		REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbServicios_acce_tbUsuarios_serv_UsuModificacion	FOREIGN KEY(serv_UsuModificacion)	REFERENCES acce.tbUsuarios(user_Id),	
);

--********TABLA INSUMOS/SERVICIOS  ****************---
GO
CREATE TABLE pbli.tbInsumosPorServicios(
inse_Id						INT IDENTITY(1,1),
serv_Id						INT,
insu_Id						INT,
inse_UsuCreacion			INT				NOT NULL,
inse_FechaCreacion			DATETIME		NOT NULL CONSTRAINT DF_pbli_tbInsumosPorServicios_inse_FechaCreacion DEFAULT(GETDATE()),
inse_UsuModificacion		INT,
inse_FechaModificacion		DATETIME,
inse_Estado					BIT				NOT NULL CONSTRAINT DF_pbli_tbInsumosPorServicios_inse_Estado DEFAULT(1)
CONSTRAINT PK_pbli_tbInsumosPorServicios_inse_Id					PRIMARY KEY(inse_Id),
CONSTRAINT FK_pbli_tbInsumosPorServicios_pbli_tbServicios_serv_Id				FOREIGN KEY(serv_Id)	REFERENCES pbli.tbServicios(serv_Id),
CONSTRAINT FK_pbli_tbInsumosPorServicios_pbli_tbInsumos_insu_Id					FOREIGN key(insu_Id)	REFERENCES pbli.tbInsumos(insu_Id),
CONSTRAINT FK_pbli_tbInsumosPorServicios_acce_tbUsuarios_inse_UsuCreacion		FOREIGN KEY(inse_UsuCreacion)		REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbInsumosPorServicios_acce_tbUsuarios_inse_UsuModificacion	FOREIGN KEY(inse_UsuModificacion)	REFERENCES acce.tbUsuarios(user_Id)
);

--********TABLA FACTURAS ****************---
CREATE TABLE pbli.tbFacturas(
fact_Id						INT IDENTITY(1,1),
clie_Id						INT					NOT NULL,
empe_Id						INT					NOT NULL,
meto_Id						INT					NOT NULL,
fact_FechaCompra			DATETIME			NOT NULL,

---------Campos de auditoría----------
fact_UsuCreacion			INT					NOT NULL,
fact_FechaCreacion			DATETIME			NOT NULL CONSTRAINT DF_pbli_tbFacturas_fact_FechaCreacion DEFAULT(GETDATE()),
fact_UsuModificacion		INT,
fact_FechaModificacion		DATETIME,
fact_Estado					BIT					NOT NULL CONSTRAINT DF_pbli_tbFacturas_fact_Estado DEFAULT(1)
CONSTRAINT PK_pbli_tbFacturas_fact_Id PRIMARY KEY(fact_Id),
CONSTRAINT FK_pbli_tbFacturas_clie_Id_pbli_tbClientes_clie_Id FOREIGN KEY(clie_Id) REFERENCES pbli.tbClientes(clie_Id),
CONSTRAINT FK_pbli_tbFacturas_empe_Id_pbli_tbEmpleados_empe_Id FOREIGN KEY(empe_Id) REFERENCES pbli.tbEmpleados(empe_Id),
CONSTRAINT FK_pbli_tbFacturas_meto_Id_gral_tbMetodosdePago_meto_Id FOREIGN KEY(meto_Id) REFERENCES gral.tbMetodosdePago(meto_Id),
CONSTRAINT FK_pbli_tbFacturas_fact_UsuCreacion_acce_tbUsuarios_inse_UsuCreacion		FOREIGN KEY(fact_UsuCreacion)		REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbFacturas_fact_UsuModificacion_acce_tbUsuarios_inse_UsuModificacion	FOREIGN KEY(fact_UsuModificacion)	REFERENCES acce.tbUsuarios(user_Id)
);


--********TABLA FACTURA DETALLE ****************---
CREATE TABLE pbli.tbFacturaDetalle(
fdet_Id INT IDENTITY(1,1),
fact_Id INT NOT NULL,
serv_Id INT NOT NULL,
fdet_Cantidad INT NOT NULL,

---------Campos de auditoría----------
fdet_UsuCreacion			INT	NOT NULL,
fdet_FechaCreacion			DATETIME NOT NULL CONSTRAINT DF_pbli_tbFacturaDetalle_fdet_FechaCreacion DEFAULT(GETDATE()),
fdet_UsuModificacion		INT,
fdet_FechaModificacion		DATETIME,
fdet_Estado					BIT	NOT NULL CONSTRAINT DF_pbli_tbFacturaDetalle_fdet_Estado DEFAULT(1)
CONSTRAINT PK_pbli_tbFacturaDetalle_fdet_Id PRIMARY KEY(fdet_Id),
CONSTRAINT FK_pbli_tbFacturaDetalle_fact_Id_pbli_tbFacturas_fact_Id FOREIGN KEY(fact_Id) REFERENCES pbli.tbFacturas(fact_Id),
CONSTRAINT FK_pbli_tbFacturaDetalle_serv_Id_pbli_tbServicios_serv_Id FOREIGN KEY(serv_Id) REFERENCES pbli.tbServicios(serv_Id),
CONSTRAINT FK_pbli_tbFacturaDetalle_fact_UsuCreacion_acce_tbUsuarios_inse_UsuCreacion		FOREIGN KEY(fdet_UsuCreacion)		REFERENCES acce.tbUsuarios(user_Id),
CONSTRAINT FK_pbli_tbFacturaDetalle_fact_UsuModificacion_acce_tbUsuarios_inse_UsuModificacion	FOREIGN KEY(fdet_UsuModificacion)	REFERENCES acce.tbUsuarios(user_Id)
);












--*******	INSERTS **********--

--********** ESTADOS CIVILES TABLE ***************--
GO
INSERT INTO gral.tbEstadosCiviles (eciv_Descripcion, eciv_Estado, eciv_UsuCreacion, eciv_FechaCreacion, eciv_UsuModificacion, eciv_FechaModificacion)
VALUES	('Soltero(a)', '1', 1, GETDATE(), NULL, NULL),
		('Casado(a)', '1', 1, GETDATE(), NULL, NULL),
		('Divorciado(a)', '1', 1, GETDATE(), NULL, NULL),
		('Viudo(a)', '1', 1, GETDATE(), NULL, NULL),
		('Union Libre', '1', 1, GETDATE(), NULL, NULL)


--********** DEPARTAMENTOS TABLE ***************--
GO
INSERT INTO gral.tbDepartamentos(depa_Codigo, depa_Nombre, depa_Estado, depa_UsuCreacion, depa_FechaCreacion, depa_UsuModificacion, depa_FechaModificacion)
VALUES	('01','Atlántida', '1', 1, GETDATE(), NULL, NULL),
		('02','Colón', '1', 1, GETDATE(), NULL, NULL),
		('03','Comayagua', '1', 1, GETDATE(), NULL,NULL),
		('04','Copán', '1', 1, GETDATE(), NULL, NULL),
		('05','Cortés', '1', 1, GETDATE(), NULL, NULL),
		('06','Choluteca', '1', 1, GETDATE(), NULL, NULL),
		('07','El Paraíso', '1', 1, GETDATE(), NULL, NULL),
		('08','Francisco Morazán', '1', 1, GETDATE(), NULL, NULL),
		('09','Gracias a Dios', '1', 1, GETDATE(), NULL, NULL),
		('10','Intibucá', '1', 1, GETDATE(), NULL, NULL),
		('11','Islas de la Bahía', '1', 1, GETDATE(), NULL, NULL),
		('12','La Paz', '1', 1, GETDATE(), NULL, NULL),
		('13','Lempira', '1', 1, GETDATE(), NULL,NULL ),
		('14','Ocotepeque', '1', 1, GETDATE(), NULL, NULL),
		('15','Olancho', '1', 1, GETDATE(), NULL, NULL),
		('16','Santa Bárbara', '1', 1, GETDATE(), NULL, NULL),
		('17','Valle', '1', 1, GETDATE(), NULL, NULL),
		('18','Yoro', '1', 1, GETDATE(), NULL, NULL);



--********** MUNICIPIOS TABLE ***************--
GO
INSERT INTO gral.tbMunicipios(depa_Id, muni_Codigo, muni_Nombre, muni_Estado, muni_UsuCreacion, muni_FechaCreacion, muni_UsuModificacion, muni_FechaModificacion)
VALUES	('1','0101','La Ceiba', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0102','El Porvenir', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0103','Tela', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0104','Jutiapa', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0105','La Masica', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0106','San Francisco', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0107','Arizona', '1', 1, GETDATE(), NULL, GETDATE()),
		('1','0108','Esparta', '1', 1, GETDATE(), NULL, GETDATE()),
	

		('2','0201','Trujillo', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0202','Balfate', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0203','Iriona', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0204','Lim�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0205','Sab�', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0206','Santa Fe', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0207','Santa Rosa de Agu�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0208','Sonaguera', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0209','Tocoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('2','0210','Bonito Oriental', '1', 1, GETDATE(), NULL, GETDATE()),


		('3',		'0301',		'Comayagua', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0302',		'Ajuterique', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0303',		'El Rosario', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0304',		'Esqu�as', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0305',		'Humuya', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0306',		'La Libertad', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0307',		'Laman�', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0308',		'La Trinidad', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0309',		'Lejaman�', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0310',		'Me�mbar', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0311',		'Minas de Oro', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0312',		'Ojos de Agua', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0313',		'San Jer�nimo', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0314',		'San Jos� de Comayagua', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0315',		'San Jos� del Potrero', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0316',		'San Luis', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0317',		'San Sebasti�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0318',		'Siguatepeque', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0319',		'Villa de San Antonio', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0320',		'Las Lajas', '1', 1, GETDATE(), NULL, GETDATE()),
		('3',		'0321',		'Taulab�', '1', 1, GETDATE(), NULL, GETDATE()),


		('4',	'0401','Santa Rosa de Cop�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0402','Caba�as', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0403','Concepci�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0404','Cop�n Ruinas', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0405','Corqu�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0406','Cucuyagua', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0407','Dolores', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0408','Dulce Nombre', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0409','El Para�so', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0410','Florida', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0411','La Jigua', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0412','La Uni�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0413','Nueva Arcadia', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0414','San Agust�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0415','San Antonio', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0416','San Jer�nimo', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0417','San Jos�', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0418','San Juan de Opoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0419','San Nicol�s', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0420','San Pedro', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0421','Santa Rita', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0422','Trinidad de Cop�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('4',	'0423','Veracruz', '1', 1, GETDATE(), NULL, GETDATE()),


		('5',	'0501','San Pedro Sula', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0502','Choloma', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0503','Omoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0504','Pimienta', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0505','Potrerillos', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0506','Puerto Cort�s', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0507','San Antonio de Cort�s', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0508','San Francisco de Yojoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0509','San Manuel', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0510','Santa Cruz de Yojoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0511','Villanueva', '1', 1, GETDATE(), NULL, GETDATE()),
		('5',	'0512','La Lima', '1', 1, GETDATE(), NULL, GETDATE()),


		('6',	'0601','Choluteca', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0602','Apacilagua', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0603','Concepci�n de Mar�a', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0604','Duyure', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0605','El Corpus', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0606','El Triunfo', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0607','Marcovia', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0608','Morolica', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0609','Namasig�e', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0610','Orocuina', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0611','Pespire', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0612','San Antonio de Flores', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0613','San Isidro', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0614','San Jos�', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0615','San Marcos de Col�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('6',	'0616','Santa Ana de Yusguare', '1', 1, GETDATE(), NULL, GETDATE()),


		('7', '0701', 'Yuscar�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0702', 'Alauca', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0703', 'Danl�', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0704', 'El Para�so', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0705', 'G�inope', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0706', 'Jacaleapa', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0707', 'Liure', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0708', 'Morocel�', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0709', 'Oropol�', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0710', 'Potrerillos', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0711', 'San Antonio de Flores', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0712', 'San Lucas', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0713', 'San Mat�as', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0714', 'Soledad', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0715', 'Teupasenti', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0716', 'Texiguat', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0717', 'Vado Ancho', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0718', 'Yauyupe', '1', 1, GETDATE(), NULL, GETDATE()),
		('7', '0719', 'Trojes', '1', 1, GETDATE(), NULL, GETDATE()),


		('8', '0801', 'Distrito Central', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0802', 'Alubar�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0803', 'Cedros', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0804', 'Curar�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0805', 'El Porvenir', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0806', 'Guaimaca', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0807', 'La Libertad', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0808', 'La Venta', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0809', 'Lepaterique', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0810', 'Maraita', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0811', 'Marale', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0812', 'Nueva Armenia', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0813', 'Ojojona', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0814', 'Orica', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0815', 'Reitoca', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0816', 'Sabanagrande', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0817', 'San Antonio de Oriente', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0818', 'San Buenaventura', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0819', 'San Ignacio', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0820', 'San Juan de Flores', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0821', 'San Miguelito', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0822', 'Santa Ana', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0823', 'Santa Luc�a', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0824', 'Talanga', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0825', 'Tatumbla', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0826', 'Valle de �ngeles', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0827', 'Villa de San Francisco', '1', 1, GETDATE(), NULL, GETDATE()),
		('8', '0828', 'Vallecillo', '1', 1, GETDATE(), NULL, GETDATE()),
		
		('9', '0901', 'Puerto Lempira', '1', 1, GETDATE(), NULL, GETDATE()),
		('9', '0902', 'Brus Laguna', '1', 1, GETDATE(), NULL, GETDATE()),
		('9', '0903', 'Ahuas', '1', 1, GETDATE(), NULL, GETDATE()),
		('9', '0904', 'Juan Francisco Bulnes', '1', 1, GETDATE(), NULL, GETDATE()),
		('9', '0905', 'Ram�n Villeda Morales', '1', 1, GETDATE(), NULL, GETDATE()),
		('9', '0906', 'Wampusirpe', '1', 1, GETDATE(), NULL, GETDATE()),
		
		('10', '1001', 'La Esperanza', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1002', 'Camasca', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1003', 'Colomoncagua', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1004', 'Concepci�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1005', 'Dolores', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1006', 'Intibuc�', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1007', 'Jes�s de Otoro', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1008', 'Magdalena', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1009', 'Masaguara', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1010', 'San Antonio', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1011', 'San Isidro', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1012', 'San Juan', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1013', 'San Marcos de la Sierra', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1014', 'San Miguel Guancapla', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1015', 'Santa Luc�a', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1016', 'Yamaranguila', '1', 1, GETDATE(), NULL, GETDATE()),
		('10', '1017', 'San Francisco de Opalaca', '1', 1, GETDATE(), NULL, GETDATE()),


		('11', '1101', 'Roat�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('11', '1102', 'Guanaja', '1', 1, GETDATE(), NULL, GETDATE()),
		('11', '1103', 'Jos� Santos Guardiola', '1', 1, GETDATE(), NULL, GETDATE()),
		('11', '1104', 'Utila', '1', 1, GETDATE(), NULL, GETDATE()),


		('12', '1201', 'La Paz', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1202', 'Aguanqueterique', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1203', 'Caba�as', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1204', 'Cane', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1205', 'Chinacla', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1206', 'Guajiquiro', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1207', 'Lauterique', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1208', 'Marcala', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1209', 'Mercedes de Oriente', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1210', 'Opatoro', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1211', 'San Antonio del Norte', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1212', 'San Jos�', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1213', 'San Juan', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1214', 'San Pedro de Tutule', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1215', 'Santa Ana', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1216', 'Santa Elena', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1217', 'Santa Mar�a', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1218', 'Santiago de Puringla', '1', 1, GETDATE(), NULL, GETDATE()),
		('12', '1219', 'Yarula', '1', 1, GETDATE(), NULL, GETDATE()),


		('13', '1301', 'Gracias', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1302', 'Bel�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1303', 'Candelaria', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1304', 'Cololaca', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1305', 'Erandique', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1306', 'Gualcince', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1307', 'Guarita', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1308', 'La Campa', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1309', 'La Iguala', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1310', 'Las Flores', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1311', 'La Uni�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1312', 'La Virtud', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1313', 'Lepaera', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1314', 'Mapulaca', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1315', 'Piraera', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1316', 'San Andr�s', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1317', 'San Francisco', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1318', 'San Juan Guarita', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1319', 'San Manuel Colohete', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1320', 'San Rafael', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1321', 'San Sebasti�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1322', 'Santa Cruz', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1323', 'Talgua', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1324', 'Tambla', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1325', 'Tomal�', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1326', 'Valladolid', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1327', 'Virginia', '1', 1, GETDATE(), NULL, GETDATE()),
		('13', '1328', 'San Marcos de Caiqu�n', '1', 1, GETDATE(), NULL, GETDATE()),


		('14', '1401', 'Ocotepeque', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1402', 'Bel�n Gualcho', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1403', 'Concepci�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1404', 'Dolores Merend�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1405', 'Fraternidad', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1406', 'La Encarnaci�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1407', 'La Labor', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1408', 'Lucerna', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1409', 'Mercedes', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1410', 'San Fernando', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1411', 'San Francisco del Valle', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1412', 'San Jorge', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1413', 'San Marcos', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1414', 'Santa Fe', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1415', 'Sensenti', '1', 1, GETDATE(), NULL, GETDATE()),
		('14', '1416', 'Sinuapa', '1', 1, GETDATE(), NULL, GETDATE()),


		('15', '1501', 'Juticalpa', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1502', 'Campamento', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1503', 'Catacamas', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1504', 'Concordia', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1505', 'Dulce Nombre de Culm�', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1506', 'El Rosario', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1507', 'Esquipulas del Norte', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1508', 'Gualaco', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1509', 'Guarizama', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1510', 'Guata', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1511', 'Guayape', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1512', 'Jano', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1513', 'La Uni�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1514', 'Mangulile', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1515', 'Manto', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1516', 'Salam�', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1517', 'San Esteban', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1518', 'San Francisco de Becerra', '1',1, GETDATE(), NULL, GETDATE()),
		('15', '1519', 'San Francisco de la Paz', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1520', 'Santa Mar�a del Real', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1521', 'Silca', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1522', 'Yoc�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('15', '1523', 'Patuca', '1', 1, GETDATE(), NULL, GETDATE()),


		('16', '1601' , 'Santa B�rbara', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1602' , 'Arada', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1603' , 'Atima', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1604' , 'Azacualpa', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1605' , 'Ceguaca', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1606' , 'Concepci�n del Norte', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1607' , 'Concepci�n del Sur', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1608' , 'Chinda', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1609' , 'El N�spero', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1610' , 'Gualala', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1611' , 'Ilama', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1612' , 'Las Vegas', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1613' , 'Macuelizo', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1614' , 'Naranjito', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1615' , 'Nuevo Celilac', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1616' , 'Nueva Frontera', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1617' , 'Petoa', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1618' , 'Protecci�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1619' , 'Quimist�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1620' , 'San Francisco de Ojuera', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1621' , 'San Jos� de las Colinas', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1622' , 'San Luis', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1623' , 'San Marcos', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1624' , 'San Nicol�s', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1625' , 'San Pedro Zacapa', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1626' , 'San Vicente Centenario', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1627' , 'Santa Rita', '1', 1, GETDATE(), NULL, GETDATE()),
		('16', '1628' , 'Trinidad', '1', 1, GETDATE(), NULL, GETDATE()),


		('17', '1701', 'Nacaome', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1702', 'Alianza', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1703', 'Amapala', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1704', 'Aramecina', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1705', 'Caridad', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1706', 'Goascor�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1707', 'Langue', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1708', 'San Francisco de Coray', '1', 1, GETDATE(), NULL, GETDATE()),
		('17', '1709', 'San Lorenzo', '1', 1, GETDATE(), NULL, GETDATE()),


		('18', '1801', 'Yoro', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1802', 'Arenal', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1803', 'El Negrito', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1804', 'El Progreso', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1805', 'Joc�n', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1806', 'Morazán', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1807', 'Olanchito', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1808', 'Santa Rita', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1809', 'Sulaco', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1810', 'Victoria', '1', 1, GETDATE(), NULL, GETDATE()),
		('18', '1811', 'Yorito', '1', 1, GETDATE(), NULL, GETDATE());



--********** SUCURSALES TABLE ***************--
GO
INSERT INTO pbli.tbSucursales(sucu_Nombre, muni_Id, sucu_Direccion, sucu_UsuCreacion)
VALUES	('Sucursal Monsoon, La Ceiba', '1', 'Mall Megaplaza, Q6C5+JCG, Carr. A Tela, La Ceiba', '1'),
		('Sucursal Monsoon, San Pedro Sula', '63', 'ALTARA, HX7H+26M, C. Hacia Armenta, San Pedro Sula 21102', '1'),
		('Sucursal Monsoon, Distrito Central', '110', 'Mall Multiplaza Tegucigalpa, Frente al Hotel Real Intercontinental, Av. Roble, Tegucigalpa', '1'),
		('Sucursal Monsoon, Omoa', '65', 'MEGABLOCK, QXG8+Q9G, Unnamed Road, Omoa', '1'),
		('Sucursal Monsoon, El Progreso', '291', 'Mall Megaplaza, C59X+PJ, Carretera hacia Tela km1, CA-13, El Progreso 23201', '1');



--********** CARGOS TABLE ***************--
GO
INSERT INTO gral.tbCargos(carg_Descripcion, carg_UsuCreacion)
VALUES	('Supervisor', '1'),
		('Vendedor', '1');



--********** EMPLEADOS TABLE ***************--
GO
INSERT INTO pbli.tbEmpleados(empe_Nombres, empe_Apellidos, empe_Identidad, empe_FechaNacimiento, empe_Sexo, eciv_Id, muni_Id, empe_DireccionExacta, empe_Telefono, sucu_Id, carg_Id, empe_UsuCreacion)
VALUES	('Maria Antonia', 'Aguilar', '0101-1990-01238', '02-16-1990', 'F', '4', '1', 'Col. El Sauce, La Ceiba', '+504 3892-0126', '1', '1', '1'),
		('Oscar', 'Blanco', '0101-1992-23743', '12-30-1992', 'M', '1', '1', 'Col. La Esperanza, La Ceiba', '+504 7892-2839', '1', '2', '1'),
		('Lisa', 'Caballero', '0101-1989-73982', '04-25-1989', 'F', '1', '1', 'Col. La Flor, La Ceiba', '+504 6389-2948', '1', '2', '1'),
		('José', 'Antúnez', '0101-2000-00021', '01-01-2000', 'M', '1', '1', 'Col. Libertad, La Ceiba', '+504 8946-3846', '1', '2', '1'),
		('Lorna', 'Chaín', '0101-2002-00293', '02-05-2002', 'F', '1', '1', 'Residencial El Toronjal, La Ceiba', '+504 3628-3826', '1', '2', '1'),
		----
		('Rafael', 'Caballero', '0501-2005-00293', '01-13-2005', 'M', '1', '63', 'Col. Santa Marta, San Pedro Sula', '+504 6372-3792', '2', '1', '1'),
		('Melissa', 'Torres', '0501-2000-02353', '05-10-2000', 'F', '1', '63', 'Col. El Carmen, San Pedro Sula', '+504 5739-3827', '2', '2', '1'),
		('Junior', 'Estrada', '0501-2004-07384', '07-28-2004', 'M', '2', '63', 'Los Alamos, San Pedro Sula', '+504 3728-9303', '2', '2', '1'),
		('Jesús', 'Barreda', '0501-1975-27394', '10-10-1975', 'M', '1', '63', 'Ticamaya, San Pedro Sula', '+504 7293-8567', '2', '2', '1'),
		('Jessica', 'Ángeles', '0501-1995-83923', '11-05-1995', 'F', '2', '63', 'Casa Maya 3, San Pedro Sula', '+504 4729-8395', '2', '2', '1'),
		----
		('Wiliam', 'Afton', '0801-1985-03647', '10-05-1985', 'M', '1', '110', 'Kennedy, Tegucigalpa', '+504 6473-7483', '3', '1', '1'),
		('Roberto', 'Contreras', '0801-1974-00947', '04-15-1974', 'M', '1', '110', 'Col. Ulloa, Tegucigalpa', '+504 9858-8465', '3', '2', '1'),
		('Karen', 'Mejía', '0801-1995-09273', '03-25-1995', 'F', '1', '110', 'Col. Divino Paraíso, Tegucigalpa', '+504 7234-8212', '3', '2', '1'),
		('Roxana', 'Martínez', '0801-1980-15263', '08-12-1980', 'F', '1', '110', 'Col. Arturo Quezada, Tegucigalpa', '+504 7483-9837', '3', '2', '1'),
		('Julia', 'Calderón', '0801-1981-11823', '09-09-1981', 'F', '1', '110', 'Col. Arturo Quezada, Tegucigalpa', '+504 8790-9085', '3', '2', '1'),
		----
		('Sheila', 'Torres', '0503-1981-09283', '02-09-1981', 'F', '1', '65', 'Buena Vista, Omoa', '+504 7384-8974', '4', '1', '1'),
		('Victor', 'Mateo', '0503-1997-00829', '03-09-1997', 'M', '1', '65', 'Corinto, Omoa', '+504 9087-9056', '4', '2', '1'),
		('Amelia', 'Lara', '0503-1998-00023', '04-09-1998', 'F', '1', '65', 'La Venada, Omoa', '+504 9585-7456', '4', '2', '1'),
		('Laura', 'Serrano', '0503-1999-01724', '05-09-1999', 'F', '1', '65', 'Col. La Loma, Omoa', '+504 8869-9504', '4', '2', '1'),
		('Karla', 'Menjivar', '0503-2000-72834', '06-09-2000', 'F', '1', '65', 'Chivana, Omoa', '+504 9090-2524', '4', '2', '1'),
		----
		('Laura', 'Marano', '1804-1995-83629', '11-29-1995', 'F', '1', '291', 'Arenas Blancas, El Progreso', '+504 7384-9382', '5', '1', '1'),
		('Ross', 'Lynch', '1804-1995-18294', '12-29-1995', 'M', '1', '291', 'Diez Alborotos, El Progreso', '+504 9283-9472', '5', '2', '1'),
		('Timothée', 'Chalamet', '1804-1995-18200', '12-27-1995', 'M', '1', '291', 'Diez Alborotos, El Progreso', '+504 8273-9488', '5', '2', '1'),
		('Bill', 'Kaulitz', '1804-1989-00283', '09-01-1989', 'M', '1', '291', 'Campo Amapa, El Progreso', '+504 9984-7383', '5', '2', '1'),
		('Tom', 'Kaulitz', '1804-1989-00284', '09-01-1989', 'M', '2', '291', 'Campo Amapa, El Progreso', '+504 9863-7482', '5', '2', '1');


GO
ALTER TABLE [acce].[tbUsuarios]
ADD CONSTRAINT FK_acce_tbUsuarios_pbli_tbEmpleados_empe_Id FOREIGN KEY(empe_Id) REFERENCES pbli.tbEmpleados(empe_Id)




--********** CLIENTES TABLE ***************--
GO
INSERT INTO pbli.tbClientes(clie_Nombres, clie_Apellidos, clie_Identidad, clie_FechaNacimiento, clie_Sexo, eciv_Id, muni_Id, clie_DireccionExacta, clie_Telefono, clie_UsuCreacion)
VALUES	('Cristian', 'Aguilar', '0501-2004-98213', '02-02-2004', 'M', '1', '63', 'Sps', '+504 8989-6734', '1'),
		('Esdra', 'Cerna', '1904-1989-67251', '04-20-1989', 'F', '1', '63', 'Sps',  '+504 9341-9097', '1'),
		('Sarai', 'Quintanilla', '1109-1990-62781', '12-15-1990', 'F', '1', '63', 'Sps',  '+504 9123-5543', '1'),
		('Marco', 'Torrez', '1109-1998-28192', '09-12-1998', 'M', '1', '63', 'Sps',  '+504 8908-5463', '1'),
		('Celina', 'Arias', '0912-1990-64782', '09-12-1990', 'F', '1', '63', 'Sps',  '+504 9657-7483', '1'),
		('Luis', 'Chicas', '0910-1992-98128', '09-27-1992', 'M', '1', '63', 'Sps',  '+504 9834-5621', '1'),
		('Angie', 'Andino', '0912-1990-28739', '05-21-1990', 'F', '1', '63', 'Sps',  '+504 9064-7869', '1'),
		('Nelson', 'Umaña', '1102-1989-00090', '02-10-1989', 'M', '1', '63', 'Sps',  '+504 9345-5161', '1'),
		('Marbella', 'Gómez', '0815-1997-89023', '09-02-1997', 'F', '1', '63', 'Sps',  '+504 9809-5461', '1'),
		('Carlos', 'Amaya', '0914-1995-67281', '09-05-1995', 'M', '1', '63', 'Sps',  '+504 9109-6573', '1'),
		('Dayana', 'Erazo', '1805-1996-78934', '03-21-1995', 'F', '1', '63', 'Sps',  '+504 9563-7381', '1'),
		('Jasson', 'Zaldívar', '0912-1998-56271', '09-21-1998', 'M', '1', '63', 'Sps',  '+504 9100-7584', '1'),
		('Marlin', 'Guzmán', '0213-1994-56721', '10-07-1994', 'F', '1', '63', 'Sps',  '+504 9822-5216', '1'),
		('Yoner', 'Zaldívar', '0913-1992-45162', '09-25-1992', 'M', '1', '63', 'Sps',  '+504 8145-6627', '1'),
		('Juan', 'Sagastume', '0914-1998-20192', '09-07-1988', 'M', '1', '63', 'Sps',  '+504 9203-8749', '1'),
		('Anthony', 'Leiva', '0415-1989-62592', '11-03-1989', 'M', '1', '63', 'Sps',  '+504 9631-7521', '1'),
		('Paola', 'Decas', '0914-1996-78291', '09-23-1996', 'F', '1', '63', 'Sps',  '+504 9561-2331', '1'),
		('Caleb', 'Benítez', '1401-1990-78676', '03-27-1990', 'M', '1', '63', 'Sps',  '+504 9521-5547', '1'),
		('Exibia', 'Bueso', '0314-1998-00989', '02-15-1998', 'F', '1', '63', 'Sps',  '+504 9312-7584', '1'),
		('Carlos', 'Herrera', '0314-1990-62712', '04-22-1990', 'M', '1', '63', 'Sps',  '+504 9623-9956', '1'),
		('Ana', 'Fajardo', '0913-1990-92738', '09-23-1998', 'F', '1', '63', 'Sps',  '+504 9027-8867', '1');



--********** PROVEEDORES TABLE ***************--
GO
INSERT INTO pbli.tbProveedores(prov_Nombre, muni_Id, prov_Direccion, prov_Correo, prov_UsuCreacion)
VALUES	('AC/DC', '63', 'San Pedro Sula 21101', 'backinblack@gmail.com', '1'),
		('Twisted Sister', '110', '3RV2+VF7, Barrio Morazan, Frente a Plantas Tropicales, Boulevard Suyapa, Tegucigalpa 11101', 'iwannarock@gmail.com', '1'),
		('Guns N Roses', '63', 'Atras de Megaplaza, 2 Calle NE, Segundo Anillo Periferico, San Pedro Sula 21101', 'sweetchildomine@gmail.com', '1'),
		('Black Veil Brides', '63', ' HX6H+G43, Bulevar Armenta, San Pedro Sula 21102', 'fallenangels@gmail.com', '1'),
		('Die Ärzte', '110', '2RW2+5PH, Tegucigalpa', 'deineschuld@gmail.com', '1');


--********** CATEGORIAS TABLE ***************--
GO
INSERT INTO gral.tbCategorias(cate_Descripcion, cate_UsuCreacion)
VALUES	('Termos y tazas', '1'),		--1
		('Botones y llaveros', '1'),	--2
		('Gorras', '1'),				--3
		('Placas', '1'),				--4
		('Tarjetas', '1'),				--5
		('Banners', '1'),				--6
		('Agendas', '1'),				--7
		('Camisas', '1'),				--8
		('Material', '1');				--9


--********** INSUMOS TABLE ***************--
GO
INSERT INTO pbli.tbInsumos(insu_Nombre, cate_Id, insu_Precio, prov_Id, insu_UsuCreacion)
VALUES	('Termo Aluminio 350ML Blanco', '1', '150', '1', '1'),
		('Termo Aluminio 350ML Plata', '1', '150', '1', '1'),
		('Termo Aluminio 350ML Negro', '1', '150', '1', '1'),
		('Termo Aluminio 500ML Blanco', '1', '220', '1', '1'),
		('Termo Aluminio 500ML Plata', '1', '220', '1', '1'),
		('Termo Aluminio 500ML Negro', '1', '220', '1', '1'),
		('Botón publicitario', '2', '10', '2', '1'),
		('Gorra Blanca', '3', '60', '2', '1'),
		('Gorra Azul', '3', '60', '2', '1'),
		('Gorra Negra', '3', '60', '2', '1'),
		('Papel de sublimación', '9', '3.2', '2', '1'),
		('Llavero clip metal corazón', '2', '35', '3', '1'),
		('Llavero metal rectángulo', '2', '35', '3', '1'),
		('Llavero metal escudo', '2', '32', '3', '1'),
		('Llavero metal redondo', '2', '32', '3', '1'),
		('Placa de madera tipo hoja', '4', '200', '3', '1'),
		('Placa de madera cuadrada', '4', '200', '3', '1'),
		('Taza de cerámica blanca', '1', '36', '4', '1'),
		('Taza de acero Inox', '1', '160', '4', '1'),
		('Retratera de vidrio', '5', '310', '4', '1'),
		('Banner 32x80 Roll Up', '6', '1085', '4', '1'),
		('Agenda cosida', '7', '200', '4', '1'),
		('Agenda espiral', '7', '120', '5', '1'),
		('Lámina de acrílico transparente', '9', '100', '5', '1'),
		('Vinil negro', '9', '20', '5', '1'),
		('Vinil blanco', '9', '20', '5', '1'),
		('Vinil rojo', '9', '20', '5', '1'),
		('Vinil azul', '9', '20', '5', '1'),
		('Vinil plateado', '9', '20', '5', '1'),
		('Vinil dorado', '9', '20', '5', '1'),
		('Lámina de plástico corrugado', '9', '60', '2', '1'),
		('Camisa blanca talla S', '8', '108', '4', '1'),
		('Camisa blanca talla M', '8', '108', '4', '1'),
		('Camisa blanca talla L', '8', '108', '4', '1'),
		('Camisa blanca talla XL', '8', '108', '4', '1'),
		('Camisa negra talla S', '8', '108', '4', '1'),
		('Camisa negra talla M', '8', '108', '4', '1'),
		('Camisa negra talla L', '8', '108', '4', '1'),
		('Camisa negra talla XL', '8', '108', '4', '1'),
		('Paquete tarjetas', '5', '50', '4', '1');
	
		

--********** SERVICIOS TABLE ***************--
GO
INSERT INTO pbli.tbServicios(serv_Nombre, serv_Precio, serv_UsuCreacion)
VALUES	('Termos y tazas personalizados', '50', '1'),
		('Prendas personalizadas', '50', '1'),
		('Agendas personalizadas', '50', '1'),
		('Placas y retrateras personalizadas', '30', '1'),
		('Botones y llaveros personalizados', '10', '1'),
		('Banners', '190', '1');
GO
UPDATE	pbli.tbServicios
SET		serv_Nombre = 'Tazas y termos personalizados'
WHERE	serv_Id = '1';
GO
UPDATE	pbli.tbServicios
SET		serv_Nombre = 'Tarjetas de presentación'
WHERE	serv_Id = '4';
GO
UPDATE	pbli.tbServicios
SET		serv_Nombre = 'Botones personalizados'
WHERE	serv_Id = '5';
UPDATE	pbli.tbServicios
SET		serv_Nombre = 'Banners de publicidad'
WHERE	serv_Id = '6';
--********** INSUMOS POR SERVICIO TABLE ***************--
GO
INSERT INTO pbli.tbInsumosPorServicios(serv_Id, insu_Id, inse_UsuCreacion)
VALUES	('1', '19', '1'),
		('1', '11', '1'),
		('2', '33', '1'),
		('2', '11', '1'),
		('3', '22', '1'),
		('3', '30', '1'),
		('3', '11', '1'),
		('4', '20', '1'),
		('4', '11', '1'),
		('5', '7', '1'),
		('5', '11', '1'),
		('6', '21', '1'),
		('6', '11', '1');


--********** METODOS DE PAGO TABLE ***************--
GO
INSERT INTO gral.tbMetodosdePago(meto_Descripcion, meto_UsuCreacion)
VALUES	('Tarjeta', '1'),
		('Efectivo', '1');



--********** FACTURAS TABLE ***************--
GO
INSERT INTO pbli.tbFacturas(clie_Id, empe_Id, meto_Id, fact_FechaCompra, fact_UsuCreacion)
VALUES	('1', '1', '1', GETDATE(), '1'),
		('2', '2', '2', GETDATE(), '1'),
		('3', '1', '1', GETDATE(), '1'),
		('4', '4', '2', GETDATE(), '1'),
		('5', '1', '1', GETDATE(), '1'),
		('6', '3', '2', GETDATE(), '1'),
		('7', '8', '1', GETDATE(), '1'),
		('8', '1', '2', GETDATE(), '1'),
		('9', '9', '2', GETDATE(), '1'),
		('10', '1', '1', GETDATE(), '1');



--********** FACTURA DETALLE TABLE ***************--
GO
INSERT INTO pbli.tbFacturaDetalle(fact_Id, serv_Id, fdet_Cantidad, fdet_UsuCreacion)
VALUES	('1', '1', '2', '1'),
		('2', '2', '3', '1'),
		('3', '3', '20', '1'),
		('4', '4', '18', '1'),
		('5', '5', '21', '1'),
		('6', '6', '111', '1'),
		('7', '1', '19', '1'),
		('8', '2', '30', '1'),
		('9', '3', '21', '1'),
		('10', '4', '15', '1');

--***********CAMBIOS EN LA TABLA DE SERVICIOS**********--
GO
ALTER TABLE pbli.tbServicios
ADD serv_Url NVARCHAR(MAX);
GO
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/RYPgRkb/3.png'
WHERE	serv_Id = '1';
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/7GZqM2R/4.png'
WHERE	serv_Id = '2';
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/GtppXDM/7.png'
WHERE	serv_Id = '3';
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/F60ZMqh/6.png'
WHERE	serv_Id = '4';
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/g6j2VCY/1.png'
WHERE	serv_Id = '5';
UPDATE	pbli.tbServicios
SET		serv_Url = 'https://i.ibb.co/FwzYCdH/5.png'
WHERE	serv_Id = '6';


