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
    public class InsumosPorServicioController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public InsumosPorServicioController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpPut("Listado")]
        public IActionResult List(VW_tbInsumosPorServicio serv_Id)
        {
            var list = _publicidadService.ListadoInsumosPorServicio(serv_Id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(InsumosPorServicioViewModel insu)
        {
            var item = _mapper.Map<tbInsumosPorServicios>(insu);
            var result = _publicidadService.EliminarInsumosPorServicio(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(InsumosPorServicioViewModel insu)
        {
            var item = _mapper.Map<tbInsumosPorServicios>(insu);
            var response = _publicidadService.InsertarInsumosPorServicio(item);
            return Ok(response);
        }
    }
}
