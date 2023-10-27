using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agencia_Taxis.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : Controller
    {
        private readonly IReporteService _ReporteService;

        public ReportesController(IReporteService reporteService)
        {
            this._ReporteService = reporteService;
        }

        [HttpPost]
        ActionResult NuevoReporte(Reportes reportes)
        {
            var result = _ReporteService.NuevoReporte(reportes);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        ActionResult ConsultarReporte(int IdChofer,bool includeAll=false)
        {
            var result = _ReporteService.ConsultarReporte(IdChofer,includeAll);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        ActionResult ResolverReporte(int Id)
        {
            var result = _ReporteService.ResolverReporte(Id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        ActionResult CancelarReporte(int Id)
        {
            var result = _ReporteService.CancelarReporte(Id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [Route("Id")]
        ActionResult ReporteId(int Id)
        {
            var result = _ReporteService.ReporteId(Id);
            return StatusCode(result.StatusCode, result);
        }
    } 
}

