using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class MunicipioRepository : IRepository<tbMunicipios, VW_tbMunicipios>
    {
        public RequestStatus Delete(tbMunicipios item)
        {
            throw new NotImplementedException();
        }

        public VW_tbMunicipios Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbMunicipios item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbMunicipios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbMunicipios>(ScriptsDataBase.UDP_tbMunicipios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbMunicipios item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbMunicipios> MunicipioDDL(int id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@depa_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.Query<VW_tbMunicipios>(ScriptsDataBase.UDP_tbMunicipios_DDL, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
