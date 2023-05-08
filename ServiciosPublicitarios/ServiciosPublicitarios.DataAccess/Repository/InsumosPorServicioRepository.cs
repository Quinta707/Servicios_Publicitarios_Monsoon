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
            throw new NotImplementedException();
        }

        public VW_tbInsumosPorServicio Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbInsumosPorServicios item)
        {
            throw new NotImplementedException();
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
