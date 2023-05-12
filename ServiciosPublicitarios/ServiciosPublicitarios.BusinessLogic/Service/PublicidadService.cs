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

        public ServiceResult EliminarCliente(tbClientes item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _clienteRepository.Delete(item);
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

        public ServiceResult InsertarCliente(tbClientes item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _clienteRepository.Insert(item);
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
        public ServiceResult EditarCliente(tbClientes item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _clienteRepository.Update(item);
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

        public VW_tbClientes BuscarCliente(int? id)
        {
            try
            {
                var list = _clienteRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
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

        public ServiceResult EliminarInsumo(tbInsumos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _insumoRepository.Delete(item);
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

        public ServiceResult InsertarInsumo(tbInsumos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _insumoRepository.Insert(item);
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
        public ServiceResult EditarInsumo(tbInsumos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _insumoRepository.Update(item);
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

        public VW_tbInsumos BuscarInsumo(int? id)
        {
            try
            {
                var list = _insumoRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
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

        public VW_tbFacturas BuscarFactura(int? id)
        {
            try
            {
                var list = _facturaRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
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


        public VW_tbSucursales BuscarSucursal(int? id)
        {
            try
            {
                var list = _sucursalRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServiceResult InsertarSucursal(tbSucursales item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _sucursalRepository.Insert(item);
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

        public ServiceResult EditarSucursales(tbSucursales item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _sucursalRepository.Update(item);
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

        public ServiceResult EliminarSucursales(tbSucursales item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _sucursalRepository.Delete(item);
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

        public IEnumerable SucursalGrafica()
        {
            try
            {
                return _sucursalRepository.GraficaSucursales();

            }
            catch (Exception)
            {
                return null;
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

        public ServiceResult EliminarProveedores(tbProveedores item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _proveedorRepository.Delete(item);
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

        public ServiceResult InsertarProveedores(tbProveedores item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _proveedorRepository.Insert(item);
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
        public ServiceResult EditarProveedor(tbProveedores item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _proveedorRepository.Update(item);
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

        public VW_tbProveedores BuscarProveedor(int? id)
        {
            try
            {
                var list = _proveedorRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
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
        public ServiceResult EliminarServicio(tbServicios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _servicioRepository.Delete(item);
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

        public ServiceResult InsertarServicio(tbServicios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _servicioRepository.Insert(item);
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
        public ServiceResult EditarServicio(tbServicios item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _servicioRepository.Update(item);
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

        public VW_tbservicios BuscarServicio(int? id)
        {
            try
            {
                var list = _servicioRepository.Find(id);
                return list;
            }
            catch (Exception)
            {
                return null;
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
