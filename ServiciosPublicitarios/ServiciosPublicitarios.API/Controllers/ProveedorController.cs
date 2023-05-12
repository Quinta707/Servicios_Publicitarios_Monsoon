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
    public class ProveedorController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public ProveedorController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoProveedores();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(ProveedorViewModel proveedor)
        {
            var item = _mapper.Map<tbProveedores>(proveedor);
            var result = _publicidadService.EliminarProveedores(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(ProveedorViewModel proveedor)
        {
            var item = _mapper.Map<tbProveedores>(proveedor);
            var response = _publicidadService.InsertarProveedores(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(ProveedorViewModel proveedor)
        {
            var item = _mapper.Map<tbProveedores>(proveedor);
            var response = _publicidadService.EditarProveedor(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarProveedor(id);
            return Ok(list);
        }
    }
}
