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
    public class ClienteController : ControllerBase
    {
        private readonly PublicidadService _publicidadService;
        private readonly IMapper _mapper;

        public ClienteController(PublicidadService publicidadService, IMapper mapper)
        {
            _publicidadService = publicidadService;
            _mapper = mapper;
        }

        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _publicidadService.ListadoClientes();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(ClienteVIewModel cliente)
        {
            var item = _mapper.Map<tbClientes>(cliente);
            var result = _publicidadService.EliminarCliente(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(ClienteVIewModel cliente)
        {
            var item = _mapper.Map<tbClientes>(cliente);
            var response = _publicidadService.InsertarCliente(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(ClienteVIewModel cliente)
        {
            var item = _mapper.Map<tbClientes>(cliente);
            var response = _publicidadService.EditarCliente(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _publicidadService.BuscarCliente(id);
            return Ok(list);
        }

    }
}
