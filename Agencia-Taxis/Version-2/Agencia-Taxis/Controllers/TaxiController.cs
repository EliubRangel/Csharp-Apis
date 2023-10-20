using System;
using Agencia_Taxis.Services;
using Agencia_Taxis.Entities;
using Microsoft.AspNetCore.Mvc;
using Agencia_Taxis.Models;
using Microsoft.EntityFrameworkCore;
using Agencia_Taxis.Services.Interfaces;
using Agencia_Taxis.Models;

namespace Agencia_Taxis.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class TaxiController : Controller
	{
        private readonly ITaxiService _taxiServices;
        public TaxiController(ITaxiService taxiService)
        {
            this._taxiServices = taxiService;
        }
        [HttpGet]
        public ActionResult get()
        {
            var result = _taxiServices.Get();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public ActionResult NuevoTaxi(Taxis taxis)
        {
            var result = _taxiServices.NuevoTaxi(taxis);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public ActionResult ActualizarTaxi(Taxis taxis)
        {
            var result = _taxiServices.ActualizarTaxi(taxis);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        public ActionResult EliminarTaxi(int id)
        {
            var result = _taxiServices.EliminarTaxi(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Placa")]
        public ActionResult TaxiPorPlaca(string placa)
        {
            var result = _taxiServices.TaxiPorPlaca(placa);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("MYM")]
        public ActionResult MarcaYModelo(string marca, string modelo)
        {
            var result = _taxiServices.MarcaYModelo(marca, modelo);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Year")]
        public ActionResult YearTaxi(string marca)
        {
            var result = _taxiServices.YearTaxi(marca);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("NotDriver")]
        public ActionResult SinChofer()
        {
            var result = _taxiServices.SinChofer();
            return StatusCode(result.StatusCode, result);
                
        }
        [HttpGet]
        [Route("Id")]
        public ActionResult TaxiId(int id)
        {
            var result = _taxiServices.TaxiId(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Honda")]
        public ActionResult MarcaHonda()
        {
            var result = _taxiServices.MarcaHonda();
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("InfoTaxi")]
        public ActionResult InformacionTaxi(int anio)
        {
            var result = _taxiServices.InformacionTaxi(anio);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("InfoRepo")]
        public ActionResult InformacionReporte()
        {
            var result = _taxiServices.InformacionReporte();
            return StatusCode(result.StatusCode, result);
        }
    }
}

