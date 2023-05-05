﻿using AutoMapper;
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
    public class CargoController : ControllerBase
    {
        private readonly GeneralService _generalService;
        private readonly IMapper _mapper;

        public CargoController(GeneralService generalService, IMapper mapper)
        {
            _generalService = generalService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _generalService.ListadoCargos();
            return Ok(list);
        }
    }
}
