using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExamen.Models;
using ApiExamen.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiExamen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiExamen.Controllers
{
   
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReciboController : Controller
    {
        private readonly IReciboRepository _dlRepo;
        private readonly IMapper _mapper;

        public ReciboController(IReciboRepository dlRepo, IMapper mapper)
        {
            _dlRepo = dlRepo;
            _mapper = mapper;
        }
        
        
        /// <summary>
        /// Consulta todos los recibos
        /// </summary>
        /// <param name="Recibo_DTO"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultarRecibos")]   
        [ProducesResponseType(200, Type = typeof(List<Recibo_DTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecibos()
        {
            var listaRecibos = _dlRepo.GetRecibos();

            var listaRecibosDto = new List<Recibo_DTO>();

            foreach (var lista in listaRecibos)
            {
                listaRecibosDto.Add(_mapper.Map<Recibo_DTO>(lista));
            }
            return Ok(listaRecibosDto);
        }


        //[HttpGet("{id:int}", Name = "UnRecibo")]
        //[Route("ObtenerRecibo")]
        //[ProducesResponseType(200, Type = typeof(Recibo_DTO))]
        //[ProducesResponseType(400)]

        //public IActionResult UnRecibo(int id)
        //{
        //    var unRecibo = _dlRepo.UnRecibo(id);

        //    if (unRecibo == null)
        //    {
        //        return NotFound();
        //    }


        //    var resultadoRecibosDto = _mapper.Map<Recibo_DTO>(unRecibo);
        //    return Ok(resultadoRecibosDto);
        //}



        /// <summary>
        /// Registra un nuevo recibo
        /// </summary>
        /// <param name="Recibo_DTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegistrarRecibo")]
        [ProducesResponseType(201, Type = typeof(Recibo_DTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult RegistraRecibo([FromBody] Recibo_DTO reciboDTO)
        {
            if (reciboDTO == null)
            {
                return BadRequest(ModelState);
            }
            var recibo = _mapper.Map<Recibo>(reciboDTO);

            if (!_dlRepo.RegistraRecibo(recibo))
            {
                ModelState.AddModelError("", $"Fallo el registro del recibo{recibo.proveedor}");
                return StatusCode(500, ModelState);
            }

            return Ok(recibo);
        }

        /// <summary>
        /// Actualiza un recibo
        /// </summary>
        /// <param name="Recibo_DTO"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("ActualizarRecibo")]
        [ProducesResponseType(201, Type = typeof(Recibo_DTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ActualizarRecibo([FromBody] Recibo_DTO reciboDTO)
        {
            if (reciboDTO == null)
            {
                return BadRequest(ModelState);
            }
            var recibo = _mapper.Map<Recibo>(reciboDTO);

            if (!_dlRepo.ActualizarRecibo(recibo))
            {
                ModelState.AddModelError("", $"Fallo la actualizaciion del recibo{recibo.proveedor}");
                return StatusCode(500, ModelState);
            }

            return Ok(recibo);
        }

        /// <summary>
        /// Borra un recibo
        /// </summary>
        /// <param name="Recibo_DTO"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("BorrarRecibo")]
        [ProducesResponseType(201, Type = typeof(Recibo_DTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult BorrarRecibo([FromBody] Recibo reciboDTO)
        {
            if (reciboDTO == null)
            {
                return BadRequest(ModelState);
            }
            var recibo = _mapper.Map<Recibo>(reciboDTO);

            if (!_dlRepo.BorrarRecibo(recibo))
            {
                ModelState.AddModelError("", $"Fallo el borrado del recibo{recibo.proveedor}");
                return StatusCode(500, ModelState);
            }

            return Ok(recibo);
        }


    }
}