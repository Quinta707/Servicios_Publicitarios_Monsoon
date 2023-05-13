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
    public class PantallaRepository : IRepository<tbPantallas, VW_tbPantallas>
    {
        public RequestStatus Delete(tbPantallas item)
        {
            throw new NotImplementedException();
        }

        public VW_tbPantallas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbPantallas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbPantallas> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbPantallas>(ScriptsDataBase.UDP_tbPantallas_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbPantallas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbPantallas> PantallasMenu(int? id, int EsAdmin)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@esAdmin", EsAdmin, DbType.Int32, ParameterDirection.Input);

            return db.Query<tbPantallas>(ScriptsDataBase.UDP_tbPantallas_Menu, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable AccesoPantallas(int esAdmin , int role_Id, int pant_Id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@esAdmin", esAdmin, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@role_Id", role_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@pant_Id", pant_Id, DbType.Int32, ParameterDirection.Input);

            return db.Query(ScriptsDataBase.UDP_AccesoPantalla, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
