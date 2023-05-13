using ServiciosPublicitarios.DataAccess.Repository;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.BusinessLogic.Service
{
    public class GeneralService
    {
        private readonly DepartamentoRepository _departamentoRepository;
        private readonly MunicipioRepository    _municipioRepository;
        private readonly CategoriaRepository    _categoriaRepository;
        private readonly MetododePagoRepository _metododePagoRepository;
        private readonly CargoRepository        _cargoRepository;
        private readonly EstadoCivilRepository _estadoCivilRepository;

        public GeneralService(  DepartamentoRepository departamentoRepository,
                                MunicipioRepository municipioRepository,
                                CategoriaRepository categoriaRepository,
                                MetododePagoRepository metododePagoRepository,
                                CargoRepository cargoRepository,
                                EstadoCivilRepository estadoCivilRepository)
        {
            _departamentoRepository = departamentoRepository;
            _municipioRepository = municipioRepository;
            _categoriaRepository = categoriaRepository;
            _metododePagoRepository = metododePagoRepository;
            _cargoRepository = cargoRepository;
            _estadoCivilRepository = estadoCivilRepository;
        }

        #region Departamento
        public ServiceResult ListadoDepartamentos()
        {
            var result = new ServiceResult();
            try
            {
                var list = _departamentoRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult EliminarDepartamentos(tbDepartamentos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _departamentoRepository.Delete(item);
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

        public ServiceResult InsertarDepartamentos(tbDepartamentos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _departamentoRepository.Insert(item);
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
        public ServiceResult EditarDepartamentos(tbDepartamentos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _departamentoRepository.Update(item);
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

        public VW_tbDepartamentos BuscarDepartamentos(int? id)
        {
            try
            {
                var list = _departamentoRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Municipio
        public ServiceResult ListadoMunicipios()
        {
            var result = new ServiceResult();
            try
            {
                var list = _municipioRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }


        public ServiceResult MunicipioDDL(int id)
        {
            var result = new ServiceResult();
            try
            {
                var list = _municipioRepository.MunicipioDDL(id);
                return result.Ok(list);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceResult EliminarMunicipios(tbMunicipios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _municipioRepository.Delete(item);
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

        public ServiceResult InsertarMunicipios(tbMunicipios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _municipioRepository.Insert(item);
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
        public ServiceResult EditarMunicipios(tbMunicipios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _municipioRepository.Update(item);
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

        public VW_tbMunicipios BuscarMunicipios(int? id)
        {
            try
            {
                var list = _municipioRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Categoria


        public ServiceResult ListadoCategorias()
        {
            var result = new ServiceResult();
            try
            {
                var list = _categoriaRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public VW_tbCategorias BuscarCategorias(int? id)
        {
            try
            {
                var list = _categoriaRepository.Find(id);
                return list;
            }
            catch (Exception)
            {

                return null;

            }
        }

        public ServiceResult InsertarCategorias(tbCategorias item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _categoriaRepository.Insert(item);
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


        public ServiceResult EditarCategorias(tbCategorias item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _categoriaRepository.Update(item);
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


        public ServiceResult EliminarCategorias(tbCategorias item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _categoriaRepository.Delete(item);
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

        #region Metodo de Pago
        public ServiceResult ListadoMetodosdePago()
        {
            var result = new ServiceResult();
            try
            {
                var list = _metododePagoRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult EliminarMetodosdePago(tbMetodosdePago item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _metododePagoRepository.Delete(item);
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

        public ServiceResult InsertarMetodosdePago(tbMetodosdePago item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _metododePagoRepository.Insert(item);
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
        public ServiceResult EditarMetodosdePago(tbMetodosdePago item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _metododePagoRepository.Update(item);
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

        public VW_tbMetodosdePago BuscarMetodosdePago(int? id)
        {
            try
            {
                var list = _metododePagoRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Cargo
        public ServiceResult ListadoCargos()
        {
            var result = new ServiceResult();
            try
            {
                var list = _cargoRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult EliminarCargos(tbCargos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _cargoRepository.Delete(item);
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

        public ServiceResult InsertarCargos(tbCargos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _cargoRepository.Insert(item);
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
        public ServiceResult EditarCargos(tbCargos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _cargoRepository.Update(item);
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

        public VW_tbCargos BuscarCargos(int? id)
        {
            try
            {
                var list = _cargoRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Estado Civil
        public ServiceResult ListadoEstadosCivile()
        {
            var result = new ServiceResult();
            try
            {
                var list = _estadoCivilRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult EliminarEstadosCiviles(tbEstadosCiviles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _estadoCivilRepository.Delete(item);
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

        public ServiceResult InsertarEstadosCiviles(tbEstadosCiviles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _estadoCivilRepository.Insert(item);
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
        public ServiceResult EditarEstadosCiviles(tbEstadosCiviles item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _estadoCivilRepository.Update(item);
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

        public VW_tbEstadosCiviles BuscarEstadosCiviles(int? id)
        {
            try
            {
                var list = _estadoCivilRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }
}
