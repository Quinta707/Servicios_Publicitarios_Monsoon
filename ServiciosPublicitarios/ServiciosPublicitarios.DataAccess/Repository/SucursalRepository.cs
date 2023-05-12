using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class SucursalRepository : IRepository<tbSucursales, VW_tbSucursales>
    {
        public RequestStatus Delete(tbSucursales item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@sucu_Id", item.sucu_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbSucursales_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbSucursales Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@sucu_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbSucursales>(ScriptsDataBase.UDP_tbSucursales_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Insert(tbSucursales item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@sucu_Nombre", item.sucu_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@sucu_Direccion", item.sucu_Direccion, DbType.String, ParameterDirection.Input);
            parametros.Add("@sucu_UsuCreacion", item.sucu_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbSucursales_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbSucursales> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbSucursales>(ScriptsDataBase.UDP_tbSucursales_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbSucursales item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@sucu_Id", item.sucu_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@sucu_Nombre", item.sucu_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@muni_Id", item.muni_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@sucu_Direccion", item.sucu_Direccion, DbType.String, ParameterDirection.Input);
            parametros.Add("@sucu_UsuModificacion", item.sucu_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbSucursales_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable GraficaSucursales()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query(ScriptsDataBase.UDP_tbSucursales_Grafica, null, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
