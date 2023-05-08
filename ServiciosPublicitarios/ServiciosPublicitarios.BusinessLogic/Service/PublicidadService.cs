using ServiciosPublicitarios.DataAccess.Repository;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.BusinessLogic.Service
{
    public class PublicidadService
    {
        private readonly EmpleadoRepository _empleadoRepository;
        private readonly ClienteRepository  _clienteRepository;
        private readonly InsumoRepository   _insumoRepository;
        private readonly ServicioRepository _servicioRepository;
        private readonly FacturaRepository  _facturaRepository;
        private readonly SucursalRepository  _sucursalRepository;
        private readonly ProveedorRepository  _proveedorRepository;
        private readonly InsumosPorServicioRepository _insumosPorServicioRepository;

        public PublicidadService (EmpleadoRepository empleadoRepository, 
                                  ClienteRepository clienteRepository,
                                  InsumoRepository insumoRepository,
                                  ServicioRepository servicioRepository,
                                  FacturaRepository facturaRepository,
                                  SucursalRepository sucursalRepository,
                                  ProveedorRepository proveedorRepository,
                                  InsumosPorServicioRepository insumosPorServicioRepository)
        {
            _empleadoRepository             = empleadoRepository;
            _clienteRepository              = clienteRepository;
            _insumoRepository               = insumoRepository;
            _servicioRepository             = servicioRepository;
            _facturaRepository              = facturaRepository;
            _sucursalRepository             = sucursalRepository;
            _proveedorRepository            = proveedorRepository;
            _insumosPorServicioRepository   = insumosPorServicioRepository;
        }



        #region Empleado
        public ServiceResult ListadoEmpleados()
        {
            var result = new ServiceResult();
            try
            {
                var list = _empleadoRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }


        public VW_tbEmpleados BuscarEmpleados(int? id)
        {
            try
            {
                var list = _empleadoRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceResult InsertarEmpleados(tbEmpleados item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _empleadoRepository.Insert(item);
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

        public ServiceResult EditarEmpleados(tbEmpleados item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _empleadoRepository.Update(item);
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

        public ServiceResult EliminarEmpleados(tbEmpleados item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _empleadoRepository.Delete(item);
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


        #region Cliente
        public ServiceResult ListadoClientes()
        {
            var result = new ServiceResult();
            try
            {
                var list = _clienteRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion


        #region Insumo
        public ServiceResult ListadoInsumos()
        {
            var result = new ServiceResult();
            try
            {
                var list = _insumoRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion


        #region Factura
        public ServiceResult ListadoFacturas()
        {
            var result = new ServiceResult();
            try
            {
                var list = _facturaRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        public ServiceResult InsertarFactura(tbFacturas item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _facturaRepository.Insert(item);
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

        public ServiceResult InsertarFacturaDetalle(tbFacturaDetalle item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _facturaRepository.InsertFdet(item);
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

        public ServiceResult EliminarFacturaDetalle(tbFacturaDetalle item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _facturaRepository.DeleteFdet(item);
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

        public IEnumerable PrecioDetalle(int id)
        {
            try
            {
                return _facturaRepository.PriceFdet(id);
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceResult ListadoFacturaDetalle(int id)
        {
            var result = new ServiceResult();
            try
            {
                var list = _facturaRepository.ListFdet(id);
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }

        #endregion


        #region Sucursal
        public ServiceResult ListadoSucursales()
        {
            var result = new ServiceResult();
            try
            {
                var list = _sucursalRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion


        #region Proveedor
        public ServiceResult ListadoProveedores()
        {
            var result = new ServiceResult();
            try
            {
                var list = _proveedorRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion


        #region Servicio
        public ServiceResult ListadoServicios()
        {
            var result = new ServiceResult();
            try
            {
                var list = _servicioRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion

        #region InsumosPorServicio
        public ServiceResult ListadoInsumosPorServicio(VW_tbInsumosPorServicio serv_Id)
        {
            var result = new ServiceResult();
            try
            {
                var list = _insumosPorServicioRepository.List(serv_Id);
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
