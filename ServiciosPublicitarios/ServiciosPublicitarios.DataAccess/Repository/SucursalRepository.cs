using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class SucursalRepository : IRepository<tbSucursales, VW_tbSucursales>
    {
        public RequestStatus Delete(tbSucursales item)
        {
            throw new NotImplementedException();
        }

        public VW_tbSucursales Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbSucursales item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbSucursales> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbSucursales>(ScriptsDataBase.UDP_tbSucursales_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbSucursales item)
        {
            throw new NotImplementedException();
        }
    }
}
