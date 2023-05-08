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
    public class FacturaRepository : IRepository<tbFacturas, VW_tbFacturas>
    {
        public RequestStatus Delete(tbFacturas item)
        {
            throw new NotImplementedException();
        }

        public VW_tbFacturas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbFacturas item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@clie_Id", item.clie_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@empe_Id", item.empe_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@meto_Id", item.meto_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@fact_UsuCreacion", item.fact_UsuCreacion, DbType.Int32, ParameterDirection.Input);
          

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbFacturas_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbFacturas> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbFacturas>(ScriptsDataBase.UDP_tbFacturas_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbFacturas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbFacturaDetalle> ListFdet(int Id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@fact_Id", Id, DbType.Int32, ParameterDirection.Input);

            return db.Query<VW_tbFacturaDetalle>(ScriptsDataBase.UDP_tbFacturaDetalle_Listado, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }


        public RequestStatus InsertFdet(tbFacturaDetalle item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@fact_Id", item.fact_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@serv_Id", item.serv_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@fdet_Cantidad", item.fdet_Cantidad, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@fdet_UsuCreacion", item.fdet_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbFacturaDetalle_Insetar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public RequestStatus DeleteFdet(tbFacturaDetalle item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@fdet_Id", item.fdet_Id, DbType.Int32, ParameterDirection.Input);
           
            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbFacturaDetalle_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable PriceFdet(int id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@fact_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.Query(ScriptsDataBase.UDP_tbFacturaDetalle_Precio, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }


    }
}
