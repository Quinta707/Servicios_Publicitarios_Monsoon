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
    public class RolporPantallaController : ControllerBase
    {
        private readonly AccessService _accessService;
        private readonly IMapper _mapper;

        public RolporPantallaController(AccessService accessService, IMapper mapper)
        {
            _accessService = accessService;
            _mapper = mapper;
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(RolPorPantallaViewModel rol)
        {
            var item = _mapper.Map<tbPantallasPorRoles>(rol);
            var response = _accessService.InsertarRolesporPantalla(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _accessService.ListadoRolesPantalla(id);
            return Ok(list);
        }

        [HttpGet("BuscarPantallasdisponibles")]
        public IActionResult PanatallasDisponibles(int? id)
        {
            var list = _accessService.PanatllasDiponibles(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(RolPorPantallaViewModel rol)
        {
            var item = _mapper.Map<tbPantallasPorRoles>(rol);
            var response = _accessService.EliminarRolesporPantalla(item);
            return Ok(response);
        }
    }
}
