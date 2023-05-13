using System;
using System.Collections.Generic;
using System.Data;
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
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@insu_Id", item.insu_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbInsumos_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbInsumos Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@insu_Id", id, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<VW_tbInsumos>(ScriptsDataBase.UDP_tbInsumos_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public RequestStatus Insert(tbInsumos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@insu_Nombre", item.insu_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@cate_Id", item.cate_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_Precio", item.insu_Precio, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@prov_Id", item.prov_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_UsuCreacion", item.insu_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbInsumos_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbInsumos> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbInsumos>(ScriptsDataBase.UDP_tbInsumos_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbInsumos item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@insu_Id", item.insu_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_Nombre", item.insu_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@cate_Id", item.cate_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_Precio", item.insu_Precio, DbType.String, ParameterDirection.Input);
            parametros.Add("@prov_Id", item.prov_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@insu_UsuModificacion", item.insu_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbInsumos_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbInsumos> InsumosDDL(int id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@cate_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.Query<VW_tbInsumos>(ScriptsDataBase.UDP_tbInsumos_DDL, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
