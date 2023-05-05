using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ServicioRepository : IRepository<tbServicios, VW_tbservicios>
    {
        public RequestStatus Delete(tbServicios item)
        {
            throw new NotImplementedException();
        }

        public VW_tbservicios Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbServicios item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbservicios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbservicios>(ScriptsDataBase.UDP_tbServicios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbServicios item)
        {
            throw new NotImplementedException();
        }
    }
}
