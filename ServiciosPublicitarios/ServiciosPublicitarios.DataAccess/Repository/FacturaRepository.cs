using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class FacturaRepository : IRepository<tbFacturas, VW_tbFacturas>
    {
        public RequestStatus Delete(tbFacturas item)
        {
            throw new NotImplementedException();
        }

        public VW_tbFacturas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbFacturas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbFacturas> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbFacturas>(ScriptsDataBase.UDP_tbFacturas_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbFacturas item)
        {
            throw new NotImplementedException();
        }
    }
}
