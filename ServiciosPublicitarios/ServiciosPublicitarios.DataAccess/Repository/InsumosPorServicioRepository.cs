using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class InsumosPorServicioRepository : IRepository<tbInsumosPorServicios, VW_tbInsumosPorServicio>
    {
        public RequestStatus Delete(tbInsumosPorServicios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@inse_Id", item.inse_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_InsumosPorServicio_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbInsumosPorServicio Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbInsumosPorServicios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@serv_Id", item.serv_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_Id", item.insu_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@inse_UsuCreacion", item.inse_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_InsumosPorServicio_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbInsumosPorServicio> List(VW_tbInsumosPorServicio item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parameter = new DynamicParameters();
            parameter.Add("serv_Id", item.serv_Id, DbType.Int32, ParameterDirection.Input);
            return db.Query<VW_tbInsumosPorServicio>(ScriptsDataBase.UDP_InsumosPorServicio_Listado, parameter, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<VW_tbInsumosPorServicio> List()
        {
            throw new NotImplementedException();
        }

        public RequestStatus Update(tbInsumosPorServicios item)
        {
            throw new NotImplementedException();
        }
    }
}
