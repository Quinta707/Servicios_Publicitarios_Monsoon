using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class DepartamentoRepository : IRepository<tbDepartamentos, VW_tbDepartamentos>
    {
        public RequestStatus Delete(tbDepartamentos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@depa_Id", item.depa_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbDepartamentos_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbDepartamentos Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@depa_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbDepartamentos>(ScriptsDataBase.UDP_tbDepartamentos_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbDepartamentos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@depa_Nombre", item.depa_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@depa_UsuCreacion", item.depa_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbDepartamentos_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbDepartamentos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbDepartamentos>(ScriptsDataBase.UDP_tbDepartamentos_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbDepartamentos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@depa_Id", item.depa_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@depa_Nombre", item.depa_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@depa_UsuModificacion", item.depa_UsuModificacion, DbType.String, ParameterDirection.Input);


            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbDepartamentos_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
