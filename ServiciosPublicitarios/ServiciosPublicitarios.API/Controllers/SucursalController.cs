using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiciosPublicitarios.API.models;
using ServiciosPublicitarios.BusinessLogic.Service;
using ServiciosPublicitarios.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosPublicitarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public SucursalController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoSucursales();
            return Ok(list);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarSucursal(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(SucursalViewModel sucursal)
        {
            var item = _mapper.Map<tbSucursales>(sucursal);
            var result = _publicidadService.EliminarSucursales(item);
            return Ok(result);
        }


        [HttpPost("Insertar")]
        public IActionResult Insert(SucursalViewModel sucursal)
        {

            var item = _mapper.Map<tbSucursales>(sucursal);
            var response = _publicidadService.InsertarSucursal(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(SucursalViewModel sucursal)
        {

            var item = _mapper.Map<tbSucursales>(sucursal);
            var response = _publicidadService.EditarSucursales(item);
            return Ok(response);
        }

        [HttpGet("GraficaSucursal")]
        public IActionResult GraficaSucursal()
        {
            var list = _publicidadService.SucursalGrafica();
            return Ok(list);
        }
    }
}
