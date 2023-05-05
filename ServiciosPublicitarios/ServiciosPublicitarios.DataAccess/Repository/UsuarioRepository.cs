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
            throw new NotImplementedException();
        }

        public VW_tbUsuarios Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbUsuarios item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbUsuarios> List()
        {
            using var db = new SqlConnection(MonsoonContext.ConnectionString);
            return db.Query<VW_tbUsuarios>(ScriptsDataBase.UDP_tbusuarios_Listado, null, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbUsuarios item)
        {
            throw new NotImplementedException();
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


    }
}
