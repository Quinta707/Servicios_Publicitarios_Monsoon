﻿using AutoMapper;
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
    public class DepartamentoController : ControllerBase
    {
        private readonly GeneralService _generalService;
        private readonly IMapper _mapper;

        public DepartamentoController(GeneralService generalService, IMapper mapper)
        {
            _generalService = generalService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _generalService.ListadoDepartamentos();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(DepartamentoViewModel departamento)
        {
            var item = _mapper.Map<tbDepartamentos>(departamento);
            var result = _generalService.EliminarDepartamentos(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(DepartamentoViewModel departamento)
        {
            var item = _mapper.Map<tbDepartamentos>(departamento);
            var response = _generalService.InsertarDepartamentos(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(DepartamentoViewModel departamento)
        {
            var item = _mapper.Map<tbDepartamentos>(departamento);
            var response = _generalService.EditarDepartamentos(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _generalService.BuscarDepartamentos(id);
            return Ok(list);
        }
    }
}
