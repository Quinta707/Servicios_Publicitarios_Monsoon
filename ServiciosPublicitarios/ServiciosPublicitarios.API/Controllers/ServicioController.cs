using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiciosPublicitarios.BusinessLogic.Service;
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
    }
}
