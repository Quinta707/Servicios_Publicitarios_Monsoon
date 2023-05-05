using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class MetododePagoRepository : IRepository<tbMetodosdePago, VW_tbMetodosdePago>
    {
        public RequestStatus Delete(tbMetodosdePago item)
        {
            throw new NotImplementedException();
        }

        public VW_tbMetodosdePago Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbMetodosdePago item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbMetodosdePago> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbMetodosdePago>(ScriptsDataBase.UDP_tbMetodosdePago_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbMetodosdePago item)
        {
            throw new NotImplementedException();
        }
    }
}
