using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ClienteRepository : IRepository<tbClientes, VW_tbClientes>
    {
        public RequestStatus Delete(tbClientes item)
        {
            throw new NotImplementedException();
        }

        public VW_tbClientes Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbClientes item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbClientes> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbClientes>(ScriptsDataBase.UDP_tbClientes_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbClientes item)
        {
            throw new NotImplementedException();
        }
    }
}
