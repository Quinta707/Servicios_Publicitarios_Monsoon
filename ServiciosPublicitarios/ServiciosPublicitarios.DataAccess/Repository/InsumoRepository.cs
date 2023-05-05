using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class InsumoRepository : IRepository<tbInsumos, VW_tbInsumos>
    {
        public RequestStatus Delete(tbInsumos item)
        {
            throw new NotImplementedException();
        }

        public VW_tbInsumos Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbInsumos item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbInsumos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbInsumos>(ScriptsDataBase.UDP_tbInsumos_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }
        
        public RequestStatus Update(tbInsumos item)
        {
            throw new NotImplementedException();
        }
    }
}
