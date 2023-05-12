using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class ServicioRepository : IRepository<tbServicios, VW_tbservicios>
    {
        public RequestStatus Delete(tbServicios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@serv_Id", item.serv_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbServicios_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbservicios Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@serv_Id", id, DbType.String, ParameterDirection.Input);
            var result = db.QueryFirst<VW_tbservicios>(ScriptsDataBase.UDP_tbServicios_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public RequestStatus Insert(tbServicios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@serv_Nombre", item.serv_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_Url", item.serv_Url, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_Precio", item.serv_Precio, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_UsuCreacion", item.serv_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbServicios_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbservicios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbservicios>(ScriptsDataBase.UDP_tbServicios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbServicios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@serv_Id", item.serv_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_Nombre", item.serv_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_Precio", item.serv_Precio, DbType.String, ParameterDirection.Input);
            parametros.Add("@serv_UsuModificacion", item.serv_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbSucursales_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
