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
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMunicipios_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbMunicipios Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@muni_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbMunicipios>(ScriptsDataBase.UDP_tbMunicipios_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbMunicipios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@muni_Nombre", item.muni_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@muni_Codigo", item.muni_Codigo, DbType.String, ParameterDirection.Input);
            parametros.Add("@depa_Id", item.depa_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@muni_UsuCreacion", item.muni_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMunicipios_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbMunicipios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbMunicipios>(ScriptsDataBase.UDP_tbMunicipios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbMunicipios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@muni_Nombre", item.muni_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@depa_Id", item.depa_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@muni_UsuModificacion", item.muni_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbMunicipios_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
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
