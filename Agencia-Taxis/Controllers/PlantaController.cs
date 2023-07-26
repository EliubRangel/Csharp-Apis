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
    public class PlantaController : Controller
    {
        private readonly AgenciaDbContext dbContext;

        public PlantaController(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            ResultApi result = new ResultApi();

            var Planta = dbContext.Planta.Include(x=>x.Taxis).ToList();
            
            result.Data = Planta;
            result.Message = "ok";
            return Ok(result);

        }

        [HttpPost]
        public ActionResult Nuevaplanta(Planta planta)
        {

            ResultApi result = new ResultApi();
            dbContext.Planta.Add(planta);
            dbContext.SaveChanges();
            result.Message = "La planta se agrego correctamente";
            result.Data = planta;
            return Ok(result);
        }
        [HttpPut]
        public ActionResult ActualizarPlanta(Planta planta)
        {
            ResultApi result = new ResultApi();
            var Pta = dbContext.Planta.FirstOrDefault(x => x.Id == planta.Id);
            if (Pta == null)
            {
                result.Message = $"No se encontro la planta con el Id {Pta.Id}";
                result.IsError = true;
                return NotFound(result);
            }
            else
            {
                Pta.Encargado = planta.Encargado;
                Pta.Direccion = planta.Direccion;
                Pta.Colonia = planta.Colonia;
                Pta.CodigoPostal = planta.CodigoPostal;
                Pta.EspaciosDisponibles = planta.EspaciosDisponibles;
                Pta.EspaciosTotales = planta.EspaciosTotales;
                Pta.FechaApertura = planta.FechaApertura;
                Pta.NumeroAfiliacion = planta.NumeroAfiliacion;

                dbContext.Update(Pta);
                dbContext.SaveChanges();
                result.Data = Pta;
                result.Message = $"Se modifico la planta con el Id {Pta.Id} correctamente";
                return Ok(result);
            }

        }
        [HttpDelete]
        public ActionResult EliminarPlanta(int Id)
        {
            ResultApi result = new ResultApi();
            var Planta = dbContext.Planta.FirstOrDefault(x => x.Id == Id);
            if (Planta == null)
            {
                result.Message = $"No se encontro la planta con el Id{Planta.Id}";
                result.IsError = true;
                result.Data = Planta;
                return NotFound(result);
            }
            dbContext.Remove(Planta);
            dbContext.SaveChanges();
            result.Message = $"Se elimino el chofer con el {Planta.Id} correctamente";
            result.Data = Planta;
            return Ok(result);

        }
        [HttpPost]
        [Route("Transladar")]
        public ActionResult TrasladarTaxi(TrasladarTaxiDto dto)
        {
            ResultApi result = new ResultApi();
            var Taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == dto.IdTaxi);
            if(Taxi== null)
            {
             result.Message=$"No se encontro el taxi con el Id {dto.IdTaxi}";
             result.IsError = true;
            return NotFound(result);
            }
            

            var Planta = dbContext.Planta
            .Include(x => x.Taxis)
            .FirstOrDefault(x => x.Id == dto.IdPlanta);
            if (Planta == null)
            {
                result.Message=$"No se encontro la Planta con el id {dto.IdPlanta}";
                result.IsError= true;
                return NotFound(result);

            }
            if(dbContext)
            if(Planta.EspaciosDisponibles>=1)
            {
               Planta.Taxis.Add(Taxi);
               Planta.EspaciosDisponibles = Planta.EspaciosDisponibles-1;

               dbContext.Update(Planta); 
               dbContext.SaveChanges();
               
               
               
            }
            else
            {
                 result.Message=$"No se encontro espacio disponible en la planta";
                result.IsError= true;
                return NotFound(result);
            }
            result.Message=$"Se translado el taxi {dto.IdTaxi} correctamente";
            return Ok(result);


        }


    }
}