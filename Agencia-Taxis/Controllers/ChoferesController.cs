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

    public class ChoferesController : Controller
    {
        private readonly AgenciaDbContext dbContext;

        public ChoferesController(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        [HttpPost]
        public ActionResult NuevoChofer(Choferes choferes)
        {
            //validar el obj cliente
            dbContext.Choferes.Add(choferes);
            dbContext.SaveChanges();
            return Ok(choferes);
        }
         [HttpGet]
        public ActionResult Get()
        {
            var choferes = dbContext.Choferes
                .Include(x => x.Taxis)
                .ToList();
            return Ok(choferes);
        }
        [HttpPut]
        public ActionResult ActualizarChofer(Choferes choferes)
        {
            var cte = dbContext.Choferes.FirstOrDefault(x => x.Id == choferes.Id);
            cte.Nombre = choferes.Nombre;
            cte.Apellido = choferes.Apellido;
            cte.NumeroLicencia= choferes.NumeroLicencia;
            cte.FechaExpiracion= choferes.FechaExpiracion;
            cte.FechaNacimiento= choferes.FechaNacimiento;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            return Ok(cte);
        }
         [HttpDelete]
        public ActionResult EliminarChofer(int Id)
        {
            var cte =dbContext.Choferes.FirstOrDefault(x=> x.Id == Id);
            if(cte == null)
            {
               return NotFound("No se encontro el chofer con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            return Ok(cte);
            
        }
        [HttpPost]
        [Route("taxi")]
        public ActionResult AsignarTaxi(AsignarTaxiDto dto)
        {
            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == dto.IdTaxi);
            var chofer = dbContext.Choferes
                .Include(x => x.Taxis)
                .FirstOrDefault(x => x.Id == dto.IdChofer);
            if(chofer.Taxis.Count < 3){
                chofer.Taxis.Add(taxi);
                dbContext.SaveChanges();
            }
            return Ok("OK");
        }
    }


}