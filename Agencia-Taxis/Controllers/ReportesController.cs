using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Agencia_Taxis.models;
namespace Agencia_Taxis.Controllers

{
    [ApiController]
    [Route("[Controller]")]
    public class ReportesController : Controller
    {

        private readonly AgenciaDbContext dbContext;

        public ReportesController(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }

        [HttpPost]
        public ActionResult NuevoReporte(Reportes reportes)
        {
            ResultApi result = new ResultApi();
            if(reportes.TaxiId == 0)
            {
                result.Message = "Id taxi no puede ser 0";
                return BadRequest(result);
            }
            dbContext.Reportes.Add(reportes);
            dbContext.SaveChanges();
            result.Message = "Se agrego reporte correctamente";
            result.Data = reportes;
            result.Message = "OK";
            return Ok(result);
        }

        [HttpGet]
        public ActionResult ConsultarReporte(int idChofer)
        {
            // resultapi result = new resultapi();

            // dbContext.Reportes
            //        .Where(reporte => reporte.Estatus == Estatus.Abierto
            //          && reporte.Chofer.Id == idChofer) //Where filtra todos los elementos que cumplan con una condicion. Aun no ha ido a la db
            //        .ToList() // Va a la db y trae los registros que cumplieron con la condicion del where
            //        ;
            // result.Data = idChofer;
            // result.Message = "Ok";
            return Ok("result");
        }

    }

}
