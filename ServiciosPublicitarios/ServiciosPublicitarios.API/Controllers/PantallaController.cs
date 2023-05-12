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
    public class PantallaController : ControllerBase
    {
        private readonly AccessService _accessService;
        private readonly IMapper _mapper;

        public PantallaController(AccessService accessService, IMapper mapper)
        {
            _accessService = accessService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _accessService.ListadoPantallas();
            return Ok(list);
        }


        [HttpGet("PantallaMenu")]
        public IActionResult PantallasMenu(int? id)
        {
            var list = _accessService.PantallasMenu(id);
            return Ok(list);
        }
    }
}
