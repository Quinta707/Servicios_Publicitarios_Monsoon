using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ProveedorRepository : IRepository<tbProveedores, VW_tbProveedores>
    {
        public RequestStatus Delete(tbProveedores item)
        {
            throw new NotImplementedException();
        }

        public VW_tbProveedores Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbProveedores item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbProveedores> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbProveedores>(ScriptsDataBase.UDP_tbProveedores_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbProveedores item)
        {
            throw new NotImplementedException();
        }
    }
}
