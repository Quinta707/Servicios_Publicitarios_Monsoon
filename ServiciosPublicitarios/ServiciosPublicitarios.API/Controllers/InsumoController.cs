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
    public class InsumoController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public InsumoController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoInsumos();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(InsumoViewModel proveedor)
        {
            var item = _mapper.Map<tbInsumos>(proveedor);
            var result = _publicidadService.EliminarInsumo(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(InsumoViewModel proveedor)
        {
            var item = _mapper.Map<tbInsumos>(proveedor);
            var response = _publicidadService.InsertarInsumo(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(InsumoViewModel proveedor)
        {
            var item = _mapper.Map<tbInsumos>(proveedor);
            var response = _publicidadService.EditarInsumo(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarInsumo(id);
            return Ok(list);
        }

        [HttpPut("InsumoDDL")]
        public IActionResult InsumoDDL(int id)
        {
            var list = _publicidadService.InsumosDDL(id);
            return Ok(list);
        }
    }
}
