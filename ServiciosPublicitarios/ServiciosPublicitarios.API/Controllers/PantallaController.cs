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
        public IActionResult PantallasMenu(int? id, int EsAdmin)
        {
            var list = _accessService.PantallasMenu(id, EsAdmin);
            return Ok(list);
        }

        [HttpPut("AccesoPantalla")]
        public IActionResult FindPrice(int esAdmin, int role_Id, int pant_Id)
        {
            var list = _accessService.AccesoPantall(esAdmin,role_Id,pant_Id);
            return Ok(list);
        }

    }
}
