using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Agencia_Taxis.Services;
using Agencia_Taxis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencia_Taxis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChoferesController : Controller
    {
        private readonly IChoferService _choferService;

        public ChoferesController(IChoferService choferService)
        {
            this._choferService = choferService;
        }

        [HttpPost]
        [Route("taxi")]
        public ActionResult AsignarTaxi(AsignarTaxiDto dto)
        {
            var result = _choferService.AsignarTaxi(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("newdriver")]
        public ActionResult NuevoChofer(Choferes choferes)
        {
            //5929/choferes/newdriver
            var result = _choferService.NuevoChofer(choferes);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public ActionResult get()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        [Route("actualizar")]
        public ActionResult ActualizarChofer(Choferes choferes)
        {
            var result = _choferService.ActualizarChofer(choferes);
            return StatusCode(result.StatusCode, result);
                
        }
        [HttpDelete]
        [Route("eliminar")]
        public ActionResult Eliminar(int id)
        {
            var result = _choferService.Eliminar(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("mayor")]
        public ActionResult MayorEdad()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("expirada")]
        public ActionResult LicenciaExpirada()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Notaxi")]
        public ActionResult SinTaxis()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("statusabierto")]
        public ActionResult ChoferEstatusAbierto()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Id")]
        public ActionResult ChoferId()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Contaxi")]
        public ActionResult ConTaxi()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }
    }
}

