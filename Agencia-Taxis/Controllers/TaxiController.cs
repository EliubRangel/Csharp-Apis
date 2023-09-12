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
    public class TaxiController : Controller
    {
        private readonly AgenciaDbContext dbContext;

        public TaxiController(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        [HttpPost]
        public ActionResult NuevoTaxi(Taxis taxis)
        {
            //validar el obj cliente
            ResultApi result = new ResultApi();
            dbContext.Taxis.Add(taxis);
            dbContext.SaveChanges();
            result.Message = "Se agrego el taxi correctamente";
            result.Data = taxis;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult Get()
        {
            ResultApi result = new ResultApi();

            var taxis = dbContext.Taxis.ToList();
            result.Data = taxis;
            result.Message = "ok";
            return Ok(result);
        }
        [HttpPut]
        public ActionResult ActualizarTaxi(Taxis taxis)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == taxis.Id);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {taxi.Id}";
                result.IsError = true;
                return NotFound(result);
            }
            else
            {
                taxi.Marca = taxis.Marca;
                taxi.Modelo = taxis.Modelo;
                taxi.Anio = taxis.Anio;
                taxi.Placas = taxis.Placas;
                taxi.NumeroPlaca = taxis.NumeroPlaca;

                dbContext.Update(taxi);
                dbContext.SaveChanges();
                result.Data = taxi;
                result.Message = $"Se modifico el taxi con el Id {taxi.Id} correctamente";
                return Ok(result);
            }

        }

        [HttpDelete]
        public ActionResult EliminarTaxi(int Id)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == Id);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id{taxi.Id}";
                result.IsError = true;
                result.Data = taxi;
                return NotFound("No se encontro el taxi con el Id");
            }
            dbContext.Remove(taxi);
            dbContext.SaveChanges();
            result.Message = $"Se elimino taxi con el Id {taxi.Id} correctamente";
            result.Data = taxi;
            return Ok(result);

        }

        [HttpGet]
        [Route("placa/{placa}")]
        public ActionResult TaxiPorPlaca(string placa)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .FirstOrDefault(x => x.NumeroPlaca == placa);
            if (taxi == null)
            {
                result.Message = $"No se encontro la placa con la numeracion {taxi}";
                result.IsError = true;
                result.Data = taxi;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi;
            return Ok(result);
        }
        [HttpGet]
        [Route("{marca}/{modelo}")]
        public ActionResult MarcaYModelo(string marca, string modelo)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .FirstOrDefault(x => x.Marca == marca && x.Modelo == modelo);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con la marca {marca} y el modelo {modelo}";
                result.IsError = true;
                result.Data = taxi;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi;
            return Ok(result);
        }
        [HttpGet]
        [Route("marca/{marca}")]
        public ActionResult YearTaxi(string Marca)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .FirstOrDefault(x => x.Marca == Marca);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi de la marca {taxi}";
                result.IsError = true;
                result.Data = taxi;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi.Anio;
            return Ok(result);
        }
        [HttpGet]
        [Route("SinChofer")]
        public ActionResult SinChofer()
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .Where(x => !x.Choferes.Any())
                .ToList();
            result.Data = taxi;
            result.Message = "Ok";
            return Ok(result);
        }
        [HttpGet]
        [Route("{Id}")]
        public ActionResult TaxiId(int Id)
        {
            ResultApi result = new ResultApi();
            var Taxi = dbContext
                .Taxis
                .FirstOrDefault(x => x.Id == Id);
            if (Taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {Id}";
                result.IsError = true;
                result.Data = Taxi;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Data = Taxi;
            result.Message = "Ok";
            return Ok(result);
        }
        [HttpGet]
        [Route("honda")]
        public ActionResult MarcaHonda()
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .Where(x => x.Marca == "honda")
                .Select(x => x.NumeroPlaca)
                .ToList();
            if(taxi == null)
            {
                result.Message = "No se encontraron taxis de de marca Honda ";
                result.Data = taxi;
                result.IsError = true;
                return BadRequest(result);
            }
            result.Data = taxi;
            result.Message = "Ok";
            return Ok(result);
            
        }
        [HttpGet]
        [Route("anio")]
        public ActionResult InformacionTaxi(int Anio)
        {
            ResultApi result = new ResultApi();
            var informacion = dbContext
                .Taxis
                .Where(x => x.Anio == Anio)
                .Select((Taxis x) => new InformacionTaxiDto
                {
                    Marca = x.Marca,
                    Modelo = x.Modelo,
                    Anio = x.Anio
                }).FirstOrDefault();
            if(informacion== null)
            {
                result.Message = $"No se encontraron vehiculos con el anio {Anio}";
                result.Data = informacion;
                result.IsError = true;
                return BadRequest(result);
            }
            result.Data = informacion;
            result.Message = "Ok";
            return Ok(result);
        }
        [HttpGet]
        [Route("informacionReportes")]
        public ActionResult InformacionReporte()
        {
            ResultApi result = new ResultApi();
            var informacion=dbContext
                .Reportes
                .Where(x=> x.Estatus == Estatus.Abierto)
                .Select((Taxis x, Choferes j, Reportes a)=> new InformacionTaxiDto
                {
                  Marca= x.Marca,
                  Modelo= x.Modelo,
                  Placas= x.NumeroPlaca,
                  RazonMulta= a.RazonMulta,
                  NombreChofer=j.Nombre
                })

        }

    }
}