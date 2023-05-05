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
    }
}
