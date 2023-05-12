using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ProveedorRepository : IRepository<tbProveedores, VW_tbProveedores>
    {
        public RequestStatus Delete(tbProveedores item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@prov_Id", item.prov_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbProveedores_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbProveedores Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@prov_Id", id, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<VW_tbProveedores>(ScriptsDataBase.UDP_tbProveedores_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public RequestStatus Insert(tbProveedores item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@prov_Nombre", item.prov_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_Direccion", item.prov_Direccion, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_Correo", item.prov_Correo, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_UsuCreacion", item.prov_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbProveedores_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbProveedores> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbProveedores>(ScriptsDataBase.UDP_tbProveedores_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbProveedores item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@prov_Id", item.prov_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@prov_Nombre", item.prov_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_Direccion", item.prov_Direccion, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_Correo", item.prov_Correo, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_UsuModificacion", item.prov_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbProveedores_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
