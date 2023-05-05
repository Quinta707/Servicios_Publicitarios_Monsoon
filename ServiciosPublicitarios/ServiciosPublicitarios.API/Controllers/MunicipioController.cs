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
    public class MunicipioController : ControllerBase
    {
        private readonly GeneralService _generalService;
        private readonly IMapper _mapper;

        public MunicipioController(GeneralService generalService, IMapper mapper)
        {
            _generalService = generalService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _generalService.ListadoMunicipios();
            return Ok(list);
        }

        [HttpPut("MunicipioDDL")]
        public IActionResult MunicipioDDL(int id)
        {
            var list = _generalService.MunicipioDDL(id);
            return Ok(list);
        }

    }
}
