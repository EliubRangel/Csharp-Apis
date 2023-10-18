using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Agencia_Taxis.Services;
using Agencia_Taxis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agencia_Taxis.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class PlantaController: Controller
	{
        private readonly IPlantaService _plantaService;

        public PlantaController(IPlantaService plantaService)
        {
            this._plantaService = plantaService;
        }


        [HttpGet]
        public ActionResult get()
        {
            var result = _plantaService.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public ActionResult NuevaPlanta(Planta planta)
        {
            var result = _plantaService.Nuevaplanta(planta);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public ActionResult ActualizarPlanta(Planta planta)
        {
            var result = _plantaService.ActualizarPlanta(planta);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        public ActionResult EliminarPlanta(int Id)
        {
            var result = _plantaService.EliminarPlanta(Id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [Route("trasladar")]
        public ActionResult TrasladarTaxi(TrasladarTaxiDto dto)
        {
            var result = _plantaService.TrasladarTaxi(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("plantacp")]
        public ActionResult PlantaCp(string cp)
        {
            var result = _plantaService.PlantaCp(cp);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("sintaxi")]
        public ActionResult PlantaSinTaxi()
        {
            var result = _plantaService.PlantaSinTaxis();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("fechas")]
        public ActionResult PlantaFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            var result = _plantaService.PlantaFechas(FechaInicio, FechaFin);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("datos")]
        public ActionResult DatosPlanta(int Id)
        {
            var result = _plantaService.DatosPlantas(Id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("disponibles")]
        public ActionResult EspaciosDisponibles()
        {
            var result = _plantaService.EspaciosDisponibles();
            return StatusCode(result.StatusCode, result);
        }
    }
}

