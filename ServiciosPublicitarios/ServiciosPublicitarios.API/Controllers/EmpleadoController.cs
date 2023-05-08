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
    public class EmpleadoController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public EmpleadoController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoEmpleados();
            return Ok(list);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarEmpleados(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(EmpleadoViewModel empleado)
        {
            var item = _mapper.Map<tbEmpleados>(empleado);
            var result = _publicidadService.EliminarEmpleados(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]

        public IActionResult Insert(EmpleadoViewModel empleado)
        {
            var item = _mapper.Map<tbEmpleados>(empleado);
            var response =_publicidadService.InsertarEmpleados(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(EmpleadoViewModel empleado)
        {
            var item = _mapper.Map<tbEmpleados>(empleado);
            var response = _publicidadService.EditarEmpleados(item);
            return Ok(response);
        }
    }
}
