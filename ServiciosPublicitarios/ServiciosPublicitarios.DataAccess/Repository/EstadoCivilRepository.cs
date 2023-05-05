using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class EstadoCivilRepository : IRepository<tbEstadosCiviles, VW_tbEstadosCiviles>
    {
        public RequestStatus Delete(tbEstadosCiviles item)
        {
            throw new NotImplementedException();
        }

        public VW_tbEstadosCiviles Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbEstadosCiviles item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbEstadosCiviles> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbEstadosCiviles>(ScriptsDataBase.UDP_tbEstadosCiviles_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbEstadosCiviles item)
        {
            throw new NotImplementedException();
        }
    }
}
