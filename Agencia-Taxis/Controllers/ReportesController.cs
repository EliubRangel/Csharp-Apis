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
            //Se crea el objeto result, para regresar al cliente en la respuesta.
            ResultApi result = new ResultApi();
            //validar que el id del taxi que recibimos por parametro
            //exista en la tabla de taxi en db 
            var taxi = dbContext.Taxis.FirstOrDefault(t => t.Id == reportes.TaxiId);
            if(taxi == null)
            {
                result.Message = "El id del taxi no existe";
                return BadRequest(result);

            }

            //valida que el id del chofer que recibimos por paramtro
            //exista en la tabla de chofer en db
            var Chof = dbContext.Choferes.FirstOrDefault(c => c.Id == reportes.ChoferId);
            if(Chof == null)
            {
                result.Message = "El Id del Chofer no existe";
                return BadRequest(result);

            }
            reportes.Estatus = Estatus.Abierto;
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
            ResultApi result = new ResultApi();
            var Reportes= dbContext.Reportes
            .Where(reporte => reporte.Estatus == Estatus.Abierto
             && reporte.Chofer.Id == idChofer) //Where filtra todos los elementos que cumplan con una condicion. Aun no ha ido a la db
            .ToList();
            // Va a la db y trae los registros que cumplieron con la condicion del where
            
                    
            result.Data = Reportes;
            result.Message = "Ok";
            return Ok(result);
        }

    }

}
