using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class RolRepository : IRepository<tbRoles, VW_tbRoles>
    {
        public RequestStatus Delete(tbRoles item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", item.role_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbRoles_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbRoles Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbRoles>(ScriptsDataBase.UDP_tbRoles_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbRoles item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Nombre", item.role_Nombre, DbType.String, ParameterDirection.Input);
            parametros.Add("@role_UsuCreacion", item.role_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbRoles_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<VW_tbRoles> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbRoles>(ScriptsDataBase.UDP_tbRoles_Listado, null, commandType: System.Data.CommandType.StoredProcedure);

        }

        public RequestStatus Update(tbRoles item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id",              item.role_Id,                   DbType.Int32, ParameterDirection.Input);
            parametros.Add("@role_Nombre",          item.role_Nombre,               DbType.String, ParameterDirection.Input);
            parametros.Add("@role_UsuModificacion", item.role_FechaModificacion,    DbType.String, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbRoles_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public RequestStatus InsertPatallasporRol(tbPantallasPorRoles item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", item.role_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@pant_Id", item.pant_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@prol_UsuCreacion", item.prol_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbPantallasPorRoles_Insertar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public IEnumerable<VW_tbPantallasPorRoles> FindOantallasRoles(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.Query<VW_tbPantallasPorRoles>(ScriptsDataBase.UDP_tbPantallasPorRoles_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<tbPantallas> PanatllasDisponibles(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.Query<tbPantallas>(ScriptsDataBase.UDP_tbPantallasPorRoles_PantallasDisponibles, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus DeleteRolesporPanatlla(tbPantallasPorRoles item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@role_Id", item.role_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbPantallasPorRoles_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
