using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class CategoriaRepository : IRepository<tbCategorias, VW_tbCategorias>
    {
        public RequestStatus Delete(tbCategorias item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@cate_Id", item.cate_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCategorias_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbCategorias Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@cate_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbCategorias>(ScriptsDataBase.UDP_tbCategorias_Buscarr, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbCategorias item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@cate_Descripcion", item.cate_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@cate_UsuCreacion", item.cate_UsuCreacion, DbType.Int32, ParameterDirection.Input);
            
            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCategorias_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }

        public IEnumerable<VW_tbCategorias> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbCategorias>(ScriptsDataBase.UDP_tbCategorias_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbCategorias item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@cate_Id", item.cate_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@cate_Descripcion", item.cate_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@cate_UsuModificacion", item.cate_UsuModificacion, DbType.String, ParameterDirection.Input);
         

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbCategorias_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
    }
}
