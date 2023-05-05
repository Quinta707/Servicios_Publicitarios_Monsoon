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
    public class UsuarioController : ControllerBase
    {
        private readonly AccessService _accessService;
        private readonly IMapper _mapper;

        public UsuarioController(AccessService accessService, IMapper mapper)
        {
            _accessService = accessService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _accessService.ListadoUsuarios();
            return Ok(list);
        }



        [HttpPut("IniciarSesion")]
        public IActionResult Login(UsuarioViewModel usuariosView)
        {
            var item = _mapper.Map<tbUsuarios>(usuariosView);
            var list = _accessService.Login(item);
            return Ok(list);
        }

        [HttpPut("Recuperar")]
        public IActionResult Recuperar(UsuarioViewModel usuariosView)
        {
            var item = _mapper.Map<tbUsuarios>(usuariosView);
            var list = _accessService.Recuperar(item);
            return Ok(list);
        }
    }
}
