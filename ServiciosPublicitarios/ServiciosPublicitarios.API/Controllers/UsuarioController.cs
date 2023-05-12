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

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _accessService.BuscarUsuarios(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(UsuarioViewModel usuario)
        {
            var item = _mapper.Map<tbUsuarios>(usuario);
            var result = _accessService.EliminarUsuarios(item);
            return Ok(result);
        }


        [HttpPost("Insertar")]
        public IActionResult Insert(UsuarioViewModel usuario)
        {
            var item = _mapper.Map<tbUsuarios>(usuario);
            var result = _accessService.InsertarUsuarios(item);
            return Ok(result);
        }

        [HttpPost("Editar")]
        public IActionResult Update(UsuarioViewModel usuario)
        {

            var item = _mapper.Map<tbUsuarios>(usuario);
            var response = _accessService.EditarUsuarios(item);
            return Ok(response);
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

        //[HttpPut("ValidarUsuario")]
        //public IActionResult ValidarUsuario(UsuarioViewModel usuariosView)
        //{
        //    var item = _mapper.Map<tbUsuarios>(usuariosView);
        //    var list = _accessService.ValidarUsuario(item);
        //    return Ok(list);
        //}
    }
}
