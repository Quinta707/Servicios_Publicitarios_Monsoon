using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ScriptsDataBase
    {

        //----- PBLI ---------

        #region Empleado
        public static string UDP_tbEmpleados_Listado = "pbli.UDP_tbEmpleados_Index";
        public static string UDP_tbEmpleados_Insertar = "pbli.UDP_tbEmpleados_Insert";
        public static string UDP_tbEmpleados_Actualizar = "pbli.UDP_tbEmpleados_Update";
        public static string UDP_tbEmpleados_Eliminar = "pbli.UDP_tbEmpleados_Delete";
        public static string UDP_tbEmpleados_Buscar = "pbli.UDP_tbEmpleados_Find";
        #endregion

        #region Cliente
        public static string UDP_tbClientes_Listado = "pbli.UDP_tbClientes_Index";
        public static string UDP_tbClientes_Insertar = "pbli.UDP_tbClientes_Insert";
        public static string UDP_tbClientes_Actualizar = "pbli.UDP_tbClientes_Update";
        public static string UDP_tbClientes_Eliminar = "pbli.UDP_tbClientes_Delete";
        public static string UDP_tbClientes_Buscar = "pbli.UDP_tbClientes_Find";
        #endregion

        #region Insumo
        public static string UDP_tbInsumos_Listado = "pbli.UDP_tbInsumos_Index";
        public static string UDP_tbInsumos_Insertar = "pbli.UDP_tbInsumos_Insert";
        public static string UDP_tbInsumos_Actualizar = "pbli.UDP_tbInsumos_Update";
        public static string UDP_tbInsumos_Eliminar = "pbli.UDP_tbInsumos_Delete";
        public static string UDP_tbInsumos_Buscar = "pbli.UDP_tbInsumos_Find";
        #endregion

        #region Factura
        public static string UDP_tbFacturas_Listado         = "pbli.UDP_tbFacturas_Index";
        public static string UDP_tbFacturas_Insertar        = "pbli.UDP_tbFacturas_Insert";
        public static string UDP_tbFacturas_Actualiar       = "pbli.UDP_tbFacturas_Update";
        public static string UDP_tbFacturas_Eliminar        = "pbli.UDP_tbFacturas_Delete";
        public static string UDP_tbFacturas_Buscar          = "pbli.UDP_tbFacturas_Find";

        public static string UDP_tbFacturaDetalle_Insetar   = "pbli.tbFacturaDetalle_Insert";
        public static string UDP_tbFacturaDetalle_Eliminar  = "pbli.tbFacturaDetalle_Delete";
        public static string UDP_tbFacturaDetalle_Listado   = "pbli.tbFacturaDetalle_Index";
        public static string UDP_tbFacturaDetalle_Precio    = "pbli.UDP_tbFacturaDetalle_Price";
        #endregion           

        #region Sucursal
        public static string UDP_tbSucursales_Listado = "pbli.UDP_tbSucursales_Index";
        public static string UDP_tbSucursales_Insertar = "pbli.UDP_tbSucursales_Insert";
        public static string UDP_tbSucursales_Actualizar = "pbli.UDP_tbSucursales_Update";
        public static string UDP_tbSucursales_Eliminar = "pbli.UDP_tbSucursales_Delete";
        public static string UDP_tbSucursales_Buscar = "pbli.UDP_tbSucursales_Find";
        #endregion

        #region Proveedor
        public static string UDP_tbProveedores_Listado = "pbli.UDP_tbSucursales_Find";
        public static string UDP_tbProveedores_Insertar = "pbli.UDP_tbSucursales_Find";
        public static string UDP_tbProveedores_Actualizar = "pbli.UDP_tbSucursales_Find";
        public static string UDP_tbProveedores_Eliminar = "pbli.UDP_tbSucursales_Find";
        public static string UDP_tbProveedores_Buscar = "pbli.UDP_tbSucursales_Find";
        #endregion

        #region Servicio
        public static string UDP_tbServicios_Listado = "pbli.UDP_tbServicios_Index";
        public static string UDP_tbServicios_Insertar = "pbli.UDP_tbServicios_Create";
        public static string UDP_tbServicios_Actualizar = "pbli.UDP_tbServicios_Update";
        public static string UDP_tbServicios_Eliminar = "pbli.UDP_tbServicios_Delete";
        public static string UDP_tbServicios_Buscar = "pbli.UDP_tbServicios_Find";
        #endregion


        //----- GRAL ---------

        #region Departamaneto
        public static string UDP_tbDepartamentos_Listado    = "gral.UDP_tbDepartamentos_Index";
        public static string UDP_tbDepartamentos_Insertar   = "gral.UDP_tbDepartamentos_Insert";
        public static string UDP_tbDepartamentos_Actualizar = "gral.UDP_tbDepartamentos_Update";
        public static string UDP_tbDepartamentos_Eliminar   = "gral.UDP_tbDepartamentos_Delete";
        public static string UDP_tbDepartamentos_Buscar     = "gral.UDP_tbDepartamentos_Find";
        #endregion

        #region Municipio
        public static string UDP_tbMunicipios_Listado       = "gral.UDP_tbMunicipios_Index";
        public static string UDP_tbMunicipios_Insertar      = "gral.UDP_tbMunicipios_Insert";
        public static string UDP_tbMunicipios_Actualizar    = "gral.UDP_tbMunicipios_Update";
        public static string UDP_tbMunicipios_Eliminar      = "gral.UDP_tbMunicipios_Delete";
        public static string UDP_tbMunicipios_Buscar        = "gral.UDP_tbMunicipios_Find";

        public static string UDP_tbMunicipios_DDL           = "gral.tbMunicipios_DDL";
        #endregion

        #region Categoria
        public static string UDP_tbCategorias_Listado       = "gral.UDP_tbCategorias_Index";
        public static string UDP_tbCategorias_Insertar      = "gral.UDP_tbCategorias_Insert";
        public static string UDP_tbCategorias_Actualizar    = "gral.UDP_tbCategorias_Update";
        public static string UDP_tbCategorias_Eliminar      = "gral.UDP_tbCategorias_Delete";
        public static string UDP_tbCategorias_Buscarr       = "gral.UDP_tbCategorias_Find";
        #endregion

        #region Metodos de pago
        public static string UDP_tbMetodosdePago_Listado    = "gral.UDP_tbMetodosdePago_Index";
        public static string UDP_tbMetodosdePago_Insertar   = "gral.UDP_tbMetodosdePago_Insert";
        public static string UDP_tbMetodosdePago_Actualizar = "gral.UDP_tbMetodosdePago_Update";
        public static string UDP_tbMetodosdePago_Eliminar   = "gral.UDP_tbMetodosdePago_Delete";
        public static string UDP_tbMetodosdePago_Buscar     = "gral.UDP_tbMetodosdePago_Find";
        #endregion

        #region Cargos
        public static string UDP_tbCargos_Listtado      = "gral.UDP_tbCargos_Index";
        public static string UDP_tbCargos_Insertar      = "gral.UDP_tbCargos_Insert";
        public static string UDP_tbCargos_Actualizar    = "gral.UDP_tbCargos_Update";
        public static string UDP_tbCargos_Eliminar      = "gral.UDP_tbCargos_Delete";
        public static string UDP_tbCargos_Buscar        = "gral.UDP_tbCargos_Find";
        #endregion

        #region Estado Civil
        public static string UDP_tbEstadosCiviles_Listado       = "gral.UDP_tbEstadosCiviles_Index";
        public static string UDP_tbEstadosCiviles_Insertar      = "gral.UDP_tbEstadosCiviles_Insert";
        public static string UDP_tbEstadosCiviles_Actualizar    = "gral.UDP_tbEstadosCiviles_Update";
        public static string UDP_tbEstadosCiviles_Eliminar      = "gral.UDP_tbEstadosCiviles_Delete";
        public static string UDP_tbEstadosCiviles_Buscar        = "gral.UDP_tbEstadosCiviles_Find";
        #endregion


        // ---- ACCE -----

        #region Usuario
        public static string UDP_tbusuarios_Listado     = "acce.UDP_tbUsuarios_Index";
        public static string UDP_tbusuarios_Insetar     = "acce.UDP_tbusuarios_INSERT";
        public static string UDP_tbusuarios_Actualizar  = "acce.UDP_tbusuarios_UPDATE";
        public static string UDP_tbusuarios_Eliminar    = "acce.UDP_tbusuarios_DELETE";
        public static string UDP_tbusuarios_Buscar      = "acce.UDP_tbUsuarios_Find";


        public static string UDP_tbusuarios_Iniciarsesion           = "acce.UDP_IniciarSesion";
        public static string UDP_tbusuarios_RecuperarContrasenia    = "acce.UDP_RecuperarUsuario";
        #endregion

        #region Roles
        public static string UDP_tbRoles_Listado    = "acce.UDP_tbRoles_Index";
        public static string UDP_tbRoles_Insertar   = "acce.UDP_tbRoles_Insert";
        public static string UDP_tbRoles_Actualizar = "acce.UDP_tbRoles_Update";
        public static string UDP_tbRoles_Eliminar   = "acce.UDP_tbRoles_Delete";
        public static string UDP_tbRoles_Buscar     = "acce.UDP_tbRoles_Find";
        #endregion
    }
}
 
