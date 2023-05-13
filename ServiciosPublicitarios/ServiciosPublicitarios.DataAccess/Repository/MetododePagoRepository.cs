using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class MetododePagoRepository : IRepository<tbMetodosdePago, VW_tbMetodosdePago>
    {
        public RequestStatus Delete(tbMetodosdePago item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@meto_Id", item.meto_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMetodosdePago_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbMetodosdePago Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@meto_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbMetodosdePago>(ScriptsDataBase.UDP_tbMetodosdePago_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Insert(tbMetodosdePago item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@meto_Descripcion", item.meto_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@meto_UsuCreacion", item.meto_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMetodosdePago_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbMetodosdePago> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbMetodosdePago>(ScriptsDataBase.UDP_tbMetodosdePago_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbMetodosdePago item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@meto_Id", item.meto_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@meto_Descripcion", item.meto_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@meto_UsuModificacion", item.meto_UsuModificacion, DbType.String, ParameterDirection.Input);


            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMetodosdePago_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
