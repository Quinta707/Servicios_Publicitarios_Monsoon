using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class RolRepository : IRepository<tbRoles, VW_tbRoles>
    {
        public RequestStatus Delete(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public VW_tbRoles Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbRoles> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbRoles>(ScriptsDataBase.UDP_tbRoles_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbRoles item)
        {
            throw new NotImplementedException();
        }
    }
}
