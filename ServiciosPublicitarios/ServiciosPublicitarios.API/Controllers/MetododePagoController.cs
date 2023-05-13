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
    public class MetododePagoController : ControllerBase
    {
        private readonly GeneralService _generalService;
        private readonly IMapper _mapper;

        public MetododePagoController(GeneralService generalService, IMapper mapper)
        {
            _generalService = generalService;
            _mapper = mapper;
        }


        [HttpGet("Listado")]
        public IActionResult List()
        {
            var list = _generalService.ListadoMetodosdePago();
            return Ok(list);
        }

        [HttpPost("Eliminar")]
        public IActionResult Delete(MetododePagoViewModel metododePago)
        {
            var item = _mapper.Map<tbMetodosdePago>(metododePago);
            var result = _generalService.EliminarMetodosdePago(item);
            return Ok(result);
        }

        [HttpPost("Insertar")]
        public IActionResult Insert(MetododePagoViewModel metododePago)
        {
            var item = _mapper.Map<tbMetodosdePago>(metododePago);
            var response = _generalService.InsertarMetodosdePago(item);
            return Ok(response);
        }

        [HttpPost("Editar")]
        public IActionResult Update(MetododePagoViewModel metododePago)
        {
            var item = _mapper.Map<tbMetodosdePago>(metododePago);
            var response = _generalService.EditarMetodosdePago(item);
            return Ok(response);
        }

        [HttpGet("Buscar")]
        public IActionResult Find(int? id)
        {
            var list = _generalService.BuscarMetodosdePago(id);
            return Ok(list);
        }
    }
}
