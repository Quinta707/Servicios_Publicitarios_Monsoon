using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class CargoRepository : IRepository<tbCargos, VW_tbCargos>
    {
        public RequestStatus Delete(tbCargos item)
        {
            throw new NotImplementedException();
        }

        public VW_tbCargos Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbCargos item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbCargos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbCargos>(ScriptsDataBase.UDP_tbCargos_Listtado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbCargos item)
        {
            throw new NotImplementedException();
        }
    }
}
