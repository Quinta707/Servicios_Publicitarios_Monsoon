using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class CargoRepository : IRepository<tbCargos, VW_tbCargos>
    {
        public RequestStatus Delete(tbCargos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@carg_Id", item.carg_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCargos_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbCargos Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@carg_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbCargos>(ScriptsDataBase.UDP_tbCargos_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbCargos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@carg_Descripcion", item.carg_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@carg_UsuCreacion", item.carg_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCargos_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbCargos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbCargos>(ScriptsDataBase.UDP_tbCargos_Listtado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbCargos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@carg_Id", item.carg_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@carg_Descripcion", item.carg_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@carg_UsuModificacion", item.carg_UsuModificacion, DbType.String, ParameterDirection.Input);


            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCargos_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
