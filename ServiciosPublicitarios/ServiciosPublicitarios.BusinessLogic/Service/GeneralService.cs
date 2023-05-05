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
        #endregion

        #region Esatdo Civil
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
        #endregion

    }
}
