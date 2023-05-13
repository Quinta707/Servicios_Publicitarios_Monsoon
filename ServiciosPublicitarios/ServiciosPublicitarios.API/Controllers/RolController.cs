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
    public class RolController : ControllerBase
    {
        private readonly AccessService _accessService;
        private readonly IMapper _mapper;

        public RolController(AccessService accessService, IMapper mapper)
        {
            _accessService = accessService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _accessService.ListadoRoles();
            return Ok(list);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _accessService.BuscarRoles(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(RolViewModel rol)
        {
            var item = _mapper.Map<tbRoles>(rol);
            var result = _accessService.EliminarRoles(item);
            return Ok(result);
        }


        [HttpPost("Insertar")]
        public IActionResult Insert(RolViewModel rol)
        {

            var item = _mapper.Map<tbRoles>(rol);
            var response = _accessService.InsertarRoles(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(RolViewModel rol)
        {

            var item = _mapper.Map<tbRoles>(rol);
            var response = _accessService.EditarRoles(item);
            return Ok(response);
        }

    }
}
