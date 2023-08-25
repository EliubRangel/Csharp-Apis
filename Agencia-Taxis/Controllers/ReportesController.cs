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
            if (taxi == null)
            {
                result.Message = "El id del taxi no existe";
                return BadRequest(result);

            }

            //valida que el id del chofer que recibimos por paramtro
            //exista en la tabla de chofer en db
            var Chof = dbContext.Choferes.FirstOrDefault(c => c.Id == reportes.ChoferId);
            if (Chof == null)
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
        public ActionResult ConsultarReporte(int idChofer, bool includeAll = false)
        {
            ResultApi result = new ResultApi();
            var Reportes = dbContext.Reportes
                //si includeall es falso filtra con la condicion de estatus igual a abierto
                //si es true ignora la condicion de estatus igual a abierto 
                .Where(reporte => (includeAll || reporte.Estatus == Estatus.Abierto)
             && reporte.Chofer.Id == idChofer) //Where filtra todos los elementos que cumplan con una condicion. Aun no ha ido a la db
            .ToList();
            // Va a la db y trae los registros que cumplieron con la condicion del where


            result.Data = Reportes;
            result.Message = "Ok";
            return Ok(result);
        }

        [HttpPut]
        public ActionResult ResolverReporte(int ID)

        {
            ResultApi result = new ResultApi();
            var Repo = dbContext.Reportes.FirstOrDefault(x => x.Id == ID);
            if(Repo == null)
            {
                result.Message = "El Id del reporte no existe";
                result.Data = true;
                return NotFound(result);
            }

            else
            {
                Repo.Estatus = Estatus.Resuelto;
                dbContext.Update(Repo);
                dbContext.SaveChanges();
                result.Data = Repo;
                result.Message = "Se cambio el estatus correctamente";
                return Ok(result);
            }
        }
        [HttpDelete]
        public ActionResult CancelarReporte(int Id)
        {
            ResultApi result = new ResultApi();
            var Rep = dbContext.Reportes.FirstOrDefault(x => x.Id == Id);
            if(Rep == null)
            {
                result.Message = "El id del reporte no existe";
                result.Data = true;
                return NotFound(result);
            }
            else
            {
                Rep.Estatus = Estatus.Cancelado;
                dbContext.Update(Rep);
                dbContext.SaveChanges();
                result.Data = Rep;
                result.Message = "Se cancelo el reporte correctamente";
                return Ok(result);
            }
           
        }
        [HttpGet]
        [Route("ReporteId")]
        public ActionResult ReporteId (int Id)
        {
            ResultApi result = new ResultApi();
            var RepId = dbContext
                .Reportes
                .FirstOrDefault(x => x.Id == Id);
            if(RepId== null)
            {
                result.Data = RepId;
                result.IsError = true;
                result.Message = $"No se encontro el reporte con el Id {Id}";

            }
            dbContext.SaveChanges();
            result.Data = RepId;
            result.Message = "Ok";
            return Ok(result);
        }
     



    }

}
