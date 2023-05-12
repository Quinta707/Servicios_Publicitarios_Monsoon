using Dapper;
using Microsoft.Data.SqlClient;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public class UsuarioRepository : IRepository<tbUsuarios, VW_tbUsuarios>
    {
        public RequestStatus Delete(tbUsuarios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_Id", item.user_Id, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbusuarios_Eliminar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public VW_tbUsuarios Find(int? id)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_Id", id, DbType.Int32, ParameterDirection.Input);

            return db.QueryFirst<VW_tbUsuarios>(ScriptsDataBase.UDP_tbusuarios_Buscar, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Insert(tbUsuarios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_NombreUsuario",   item.user_NombreUsuario, DbType.String, ParameterDirection.Input);
            parametros.Add("@user_Contrasena",      item.user_Contrasena, DbType.String, ParameterDirection.Input);
            parametros.Add("@user_EsAdmin",         item.user_EsAdmin, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@role_Id",              item.role_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@empe_Id",              item.empe_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@user_UsuCreacion",     item.user_UsuCreacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbusuarios_Insetar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }

        public IEnumerable<VW_tbUsuarios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbUsuarios>(ScriptsDataBase.UDP_tbusuarios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbUsuarios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();

            parametros.Add("@user_Id",              item.user_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@user_EsAdmin",         item.user_EsAdmin, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@role_Id",              item.role_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@empe_Id",              item.empe_Id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@user_UsuModificacion", item.user_UsuModificacion, DbType.Int32, ParameterDirection.Input);

            var result = db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbusuarios_Actualizar, parametros, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public VW_tbUsuarios Login(tbUsuarios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_NombreUsuario", item.user_NombreUsuario, DbType.String, ParameterDirection.Input);
            parametros.Add("@user_Contrasena", item.user_Contrasena, DbType.String, ParameterDirection.Input);

            return db.QueryFirst<VW_tbUsuarios>(ScriptsDataBase.UDP_tbusuarios_Iniciarsesion, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Recuperar(tbUsuarios item)
        {
            RequestStatus result = new RequestStatus();
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_NombreUsuario", item.user_NombreUsuario, DbType.String, ParameterDirection.Input);
            parametros.Add("@user_Contrasena", item.user_Contrasena, DbType.String, ParameterDirection.Input);

            return db.QueryFirst<RequestStatus>(ScriptsDataBase.UDP_tbusuarios_RecuperarContrasenia, parametros, commandType: System.Data.CommandType.StoredProcedure);
        }

        public VW_tbUsuarios ValidarUsuario(tbUsuarios item)
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@user_NombreUsuario", item.user_NombreUsuario, DbType.String, ParameterDirection.Input);
            return db.QueryFirst<VW_tbUsuarios>(ScriptsDataBase.UDP_tbusuarios_ValidarUsuario, parametros, commandType: System.Data.CommandType.StoredProcedure);

        }
    }
}
