using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ClienteRepository : IRepository<tbClientes, VW_tbClientes>
    {
        public RequestStatus Delete(tbClientes item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@clie_Id", item.clie_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbClientes_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbClientes Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@clie_Id", id, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<VW_tbClientes>(ScriptsDataBase.UDP_tbClientes_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public RequestStatus Insert(tbClientes item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@clie_Nombres", item.clie_Nombres, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Apellidos", item.clie_Apellidos, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Identidad", item.clie_Identidad, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_FechaNacimiento", item.clie_FechaNacimiento, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Sexo", item.clie_Sexo, DbType.String, ParameterDirection.Input);
            parametros.Add("@eciv_Id", item.eciv_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@clie_DireccionExacta", item.clie_DireccionExacta, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Telefono", item.clie_Telefono, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_UsuCreacion", item.clie_UsuCreacion, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbClientes_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbClientes> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbClientes>(ScriptsDataBase.UDP_tbClientes_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbClientes item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@clie_Id", item.clie_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@clie_Nombres", item.clie_Nombres, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Apellidos", item.clie_Apellidos, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Identidad", item.clie_Identidad, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_FechaNacimiento", item.clie_FechaNacimiento, DbType.Date, ParameterDirection.Input);
            parametros.Add("@clie_Sexo", item.clie_Sexo, DbType.String, ParameterDirection.Input);
            parametros.Add("@eciv_Id", item.eciv_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@clie_DireccionExacta", item.clie_DireccionExacta, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_Telefono", item.clie_Telefono, DbType.String, ParameterDirection.Input);
            parametros.Add("@clie_UsuModificacion", item.clie_UsuModificacion, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbClientes_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
