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

            var Planta = dbContext.Planta.Include(x => x.Taxis).ToList();

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
            //pasos para actualizar un registro en ef:
            //1: consultar el registro a modificar. se necesita consultar para que ef mantenga un la relacion.


            var Pta = dbContext.Planta.FirstOrDefault(x => x.Id == planta.Id);
            if (Pta == null)
            {
                result.Message = $"No se encontro la planta con el Id {Pta.Id}";
                result.IsError = true;
                return NotFound(result);
            }
            else
            {
                //2: asignar los nuevos valores a las propiedades que se modificaran en el registro.
                Pta.Encargado = planta.Encargado;
                Pta.Direccion = planta.Direccion;
                Pta.Colonia = planta.Colonia;
                Pta.CodigoPostal = planta.CodigoPostal;
                Pta.EspaciosDisponibles = planta.EspaciosDisponibles;
                Pta.EspaciosTotales = planta.EspaciosTotales;
                Pta.FechaApertura = planta.FechaApertura;
                Pta.NumeroAfiliacion = planta.NumeroAfiliacion;
                //3: llamar al metodo update.
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
            var Taxi = dbContext.Taxis.Include(x=> x.Planta).FirstOrDefault(x => x.Id == dto.IdTaxi);
            if (Taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {dto.IdTaxi}";
                result.IsError = true;
                return NotFound(result);
            }
             
            var Planta = dbContext.Planta
                .Include(x => x.Taxis)
                .FirstOrDefault(x => x.Id == dto.IdPlanta);
            if (Planta == null)
            {
                result.Message = $"No se encontro la Planta con el id {dto.IdPlanta}";
                result.IsError = true;
                return NotFound(result);
            }
            if (Planta.EspaciosDisponibles >= 1)
            {
                Planta.Taxis.Add(Taxi);
                Planta.EspaciosDisponibles = Planta.EspaciosDisponibles - 1;
                Taxi.Planta.EspaciosDisponibles= Taxi.Planta.EspaciosDisponibles+1;
                dbContext.Update(Planta);
                dbContext.SaveChanges();
            }
            else
            {
                result.Message = $"No se encontro espacio disponible en la planta";
                result.IsError = true;
                return NotFound(result);
            }
            result.Message = $"Se translado el taxi {dto.IdTaxi} correctamente";
            return Ok(result);


        }
        [HttpGet]
        [Route("CP")]
        public ActionResult PlantaCp(string Cp)
        {
            ResultApi result = new ResultApi();
            var Plantacp = dbContext
                .Planta
                .FirstOrDefault(x => x.CodigoPostal == Cp);
            if(Plantacp == null)
            {
                result.Message = $"No se encontro la planta con el Cp {Cp}";
                result.IsError = true;
                result.Data = Plantacp;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = Plantacp;
            return Ok(result);
        }
        [HttpGet]
        [Route("PlantaSinTaxis")]
        public ActionResult PlantaSinTaxis()
        {
            ResultApi result = new ResultApi();
            var NoTaxis = dbContext
                .Planta
                .Where(x => !x.Taxis.Any())
                .ToList();
            result.Data = NoTaxis;
            result.Message = "Ok";
            return Ok(result);
        }

    }
}