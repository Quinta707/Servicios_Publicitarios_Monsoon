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
    public class ServicioController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public ServicioController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoServicios();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(ServicioViewModel serv)
        {
            var item = _mapper.Map<tbServicios>(serv);
            var result = _publicidadService.EliminarServicio(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(ServicioViewModel servicio)
        {
            var item = _mapper.Map<tbServicios>(servicio);
            var response = _publicidadService.InsertarServicio(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(ServicioViewModel servicio)
        {
            var item = _mapper.Map<tbServicios>(servicio);
            var response = _publicidadService.EditarServicio(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarServicio(id);
            return Ok(list);
        }
    }
}
