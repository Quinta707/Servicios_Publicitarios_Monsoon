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
    public class FacturaController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public FacturaController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoFacturas();
            return Ok(list);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(FacturaViewModel factura)
        {
            var item = _mapper.Map<tbFacturas>(factura);
            var response = _publicidadService.InsertarFactura(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarFactura(id);
            return Ok(list);
        }


        [HttpPut("ListadoDetalles")]
        public IActionResult ListFdet(int id)
        {
            var list = _publicidadService.ListadoFacturaDetalle(id);
            return Ok(list);
        }


        [HttpPost("InsertarDetalles")]
        public IActionResult InsertFdet(FacturaDetalleViewModel facturaFdet)
        {
            var item = _mapper.Map<tbFacturaDetalle>(facturaFdet);
            var response = _publicidadService.InsertarFacturaDetalle(item);
            return Ok(response);
        }
        

        [HttpPost("EliminarDetalles")]
        public IActionResult DeleteFdet(FacturaDetalleViewModel facturaFdet)
        {
            var item = _mapper.Map<tbFacturaDetalle>(facturaFdet);
            var response = _publicidadService.EliminarFacturaDetalle(item);
            return Ok(response);
        }


        [HttpPut("PrecioDetalles")]
        public IActionResult FindPrice(int id)
        {
            var list = _publicidadService.PrecioDetalle(id);
            return Ok(list);
        }

    }
}
