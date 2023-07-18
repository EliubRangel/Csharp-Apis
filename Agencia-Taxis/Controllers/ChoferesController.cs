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
            ResultApi result= new ResultApi();
            dbContext.Choferes.Add(choferes);
            dbContext.SaveChanges();
            result.Message="Se agrego el chofer correctamente";
            result.Data=choferes;
            return Ok(result);
        }
         [HttpGet]
        public ActionResult Get()
        {
            ResultApi result= new ResultApi();
            var choferes = dbContext.Choferes
                .Include(x => x.Taxis)
                .ToList();
                result.Data=choferes;
                result.Message="Ok";
                
            return Ok(result);
        }
        [HttpPut]
        public ActionResult ActualizarChofer(Choferes choferes)
        {
            ResultApi result= new ResultApi();
            var cte = dbContext.Choferes.FirstOrDefault(x => x.Id == choferes.Id);
            if(cte==null)
            {
                result.Message=$"No se encontro el chofer con el Id {cte.Id}";
                result.IsError=true;
                return NotFound(result);
            }
            else
          {
              cte.Nombre = choferes.Nombre;
            cte.Apellido = choferes.Apellido;
            cte.NumeroLicencia= choferes.NumeroLicencia;
            cte.FechaExpiracion= choferes.FechaExpiracion;
            cte.FechaNacimiento= choferes.FechaNacimiento;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            result.Data=cte;
            result.Message=$"Se modifico el chofer con el Id {cte.Id} correctamente";
            return Ok(result);
          }
        }
         [HttpDelete]
        public ActionResult EliminarChofer(int Id)
        {
            ResultApi result= new ResultApi();
            var chofer =dbContext.Choferes.FirstOrDefault(x=> x.Id == Id);
            if(chofer == null)
            {
               result.Message=$"No se encontro el chofer con el Id{chofer.Id}";
               result.IsError=true;
               result.Data=chofer;
               return NotFound(result);
            }
            dbContext.Remove(chofer);
            dbContext.SaveChanges();
            result.Message= $"Se elimino el chofer con el {chofer.Id} correctamente";
            result.Data=chofer;
            return Ok(result);
            
        }
        [HttpPost]
        [Route("taxi")]
        public ActionResult AsignarTaxi(AsignarTaxiDto dto)
        {   
            ResultApi result= new ResultApi();

            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == dto.IdTaxi);
            if(taxi==null)
            {
                result.Message=$"No se encontro el taxi con el Id {dto.IdTaxi}";
                result.IsError=true;
                return NotFound(result);
                
            }
            
            var chofer = dbContext.Choferes
                .Include(x => x.Taxis)
                .FirstOrDefault(x => x.Id == dto.IdChofer);
                if(chofer==null)
                {
                    result.Message=$"No se encontro el chofer con el Id {dto.IdChofer}";
                    result.IsError=true;
                    return NotFound(result);
                }
            if(chofer.Taxis.Count < 2){
                chofer.Taxis.Add(taxi);
                dbContext.SaveChanges();
            }
            else
            {
                result.Message=$"El chofer con el Id {dto.IdChofer} ya cuenta con 2 taxis";
                result.IsError=true;
                return BadRequest(result);
            }
            result.Message="Se asigno el taxi correctamente";
            

            
            
            return Ok(result);
        }
    }


}