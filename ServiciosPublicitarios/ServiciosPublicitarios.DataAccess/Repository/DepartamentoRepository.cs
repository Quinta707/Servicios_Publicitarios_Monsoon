using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class DepartamentoRepository : IRepository<tbDepartamentos, VW_tbDepartamentos>
    {
        public RequestStatus Delete(tbDepartamentos item)
        {
            throw new NotImplementedException();
        }

        public VW_tbDepartamentos Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbDepartamentos item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbDepartamentos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbDepartamentos>(ScriptsDataBase.UDP_tbDepartamentos_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbDepartamentos item)
        {
            throw new NotImplementedException();
        }
    }
}
