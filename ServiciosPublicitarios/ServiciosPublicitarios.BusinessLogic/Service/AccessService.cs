using ServiciosPublicitarios.DataAccess.Repository;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.BusinessLogic.Service
{
    public class AccessService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly RolRepository _rolRepository;
        private readonly PantallaRepository _pantallaRepository;

        public AccessService(UsuarioRepository usuarioRepository,
                            RolRepository rolRepository,
                            PantallaRepository pantallaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _pantallaRepository = pantallaRepository;
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



        public VW_tbUsuarios BuscarUsuarios(int? id)
        {
            try
            {
                var list = _usuarioRepository.Find(id);
                return list;
            }
            catch (Exception)
            {

                return null;

            }
        }

        public ServiceResult InsertarUsuarios(tbUsuarios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _usuarioRepository.Insert(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceResult EditarUsuarios(tbUsuarios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _usuarioRepository.Update(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceResult EliminarUsuarios(tbUsuarios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _usuarioRepository.Delete(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }

            }
            catch (Exception)
            {
                throw;
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

        public VW_tbRoles BuscarRoles(int? id)
        {
            try
            {
                var list = _rolRepository.Find(id);
                return list;
            }
            catch (Exception)
            {

                return null;

            }
        }

        public ServiceResult InsertarRoles(tbRoles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolRepository.Insert(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceResult EditarRoles(tbRoles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolRepository.Update(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServiceResult EliminarRoles(tbRoles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolRepository.Delete(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region RolporPantalla

        public ServiceResult InsertarRolesporPantalla(tbPantallasPorRoles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolRepository.InsertPatallasporRol(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ServiceResult ListadoRolesPantalla(int? id)
        {
            var result = new ServiceResult();
            try
            {
                var list = _rolRepository.FindOantallasRoles(id);
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult PanatllasDiponibles(int? id)
        {
            var result = new ServiceResult();
            try
            {
                var list = _rolRepository.PanatllasDisponibles(id);
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult EliminarRolesporPantalla(tbPantallasPorRoles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolRepository.DeleteRolesporPanatlla(item);
                if (map.CodeStatus > 0)
                {
                    return result.Ok(map);
                }
                else
                {
                    map.MessageStatus = (map.CodeStatus == 0) ? "404 Error de consulta" : map.MessageStatus;
                    return result.Error(map);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Pantallas
        public ServiceResult ListadoPantallas()
        {
            var result = new ServiceResult();
            try
            {
                var list = _pantallaRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult PantallasMenu(int? id, int EsAdmin)
        {
            var result = new ServiceResult();
            try
            {
                var list = _pantallaRepository.PantallasMenu(id, EsAdmin);
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public IEnumerable AccesoPantall(int esAdmin, int role_Id, int pant_Id)
        {
            try
            {
                return _pantallaRepository.AccesoPantallas(esAdmin, role_Id, pant_Id);

            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion


    }
}
