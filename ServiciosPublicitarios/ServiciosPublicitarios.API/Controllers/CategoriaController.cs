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
    public class CategoriaController : ControllerBase
    {
        private readonly GeneralService _generalService;
        private readonly IMapper _mapper;

        public CategoriaController(GeneralService generalService, IMapper mapper)
        {
            _generalService = generalService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _generalService.ListadoCategorias();
            return Ok(list);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _generalService.BuscarCategorias(id);
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(CategoriaViewModel categoria)
        {
            var item = _mapper.Map<tbCategorias>(categoria);
            var result = _generalService.EliminarCategorias(item);
            return Ok(result);
        }
        

        [HttpPost("Insertar")]
        public IActionResult Insert(CategoriaViewModel categoria)
        {

            var item = _mapper.Map<tbCategorias>(categoria);
            var response = _generalService.InsertarCategorias(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(CategoriaViewModel categoria)
        {

            var item = _mapper.Map<tbCategorias>(categoria);
            var response = _generalService.EditarCategorias(item);
            return Ok(response);
        }
    }
}
