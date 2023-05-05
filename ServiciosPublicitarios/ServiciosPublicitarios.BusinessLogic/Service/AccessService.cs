using ServiciosPublicitarios.DataAccess.Repository;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.BusinessLogic.Service
{
    public class AccessService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly RolRepository _rolRepository;

        public AccessService(UsuarioRepository usuarioRepository, 
                            RolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }

        #region Usuario

        public VW_tbUsuarios Login(tbUsuarios item)
        {
            try
            {
                var list = _usuarioRepository.Login(item);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public ServiceResult Recuperar(tbUsuarios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _usuarioRepository.Recuperar(item);
                if (map.CodeStatus > 0)
                {
                    return result.SetMessage("Usuario Recuperado", ServiceResultType.Success);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.SetMessage("Recuperacion Fallida", ServiceResultType.Conflict);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceResult ListadoUsuarios()
        {
            var result = new ServiceResult();
            try
            {
                var list = _usuarioRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion

        #region Rol
        public ServiceResult ListadoRoles()
        {
            var result = new ServiceResult();
            try
            {
                var list = _rolRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion


    }
}
