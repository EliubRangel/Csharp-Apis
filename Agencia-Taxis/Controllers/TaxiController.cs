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
            ResultApi result= new ResultApi();
            dbContext.Taxis.Add(taxis);
            dbContext.SaveChanges();
            result.Message="Se agrego el taxi correctamente";
            result.Data=taxis;
            return Ok(result);
        }
        [HttpGet]
        public ActionResult Get()
        {
            ResultApi result= new ResultApi();

            var taxis = dbContext.Taxis.ToList();
            result.Data=taxis;
            result.Message="ok";
            return Ok(result);
        }
        [HttpPut]
        public ActionResult ActualizarTaxi(Taxis taxis)
        {
            ResultApi result= new ResultApi();
            var cte = dbContext.Taxis.FirstOrDefault(x => x.Id == taxis.Id);
            if(cte==null)
            {
                result.Message=$"No se encontro el taxi con el Id {cte.Id}";
                result.IsError=true;
                return NotFound(result);
            }
            else
        {
            cte.Marca = taxis.Marca;
            cte.Modelo = taxis.Modelo;
            cte.Año = taxis.Año;
            cte.Placas = taxis.Placas;
            cte.NumeroPlaca = taxis.NumeroPlaca;

            dbContext.Update(cte);
            dbContext.SaveChanges();
             result.Data=cte;
            result.Message=$"Se modifico el taxi con el Id {cte.Id} correctamente";
            return Ok(result);
        }
            
        }
        
        [HttpDelete]
        public ActionResult EliminarTaxi(int Id)
        {
            ResultApi result= new ResultApi();
            var cte = dbContext.Taxis.FirstOrDefault(x => x.Id == Id);
            if (cte == null)
            {
                result.Message=$"No se encontro el taxi con el Id{cte.Id}";
               result.IsError=true;
               result.Data=cte;
                return NotFound("No se encontro el taxi con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            result.Message=$"Se elimino taxi con el Id {cte.Id} correctamente";
            result.Data=cte;
            return Ok(result);

        }
        [HttpGet]
        [Route("TaxiPlaca")]
        public ActionResult TaxiPorPlaca(string Placa)

        {
            ResultApi result = new ResultApi();
            var NumPlaca = dbContext
                .Taxis
                .FirstOrDefault(x => x.NumeroPlaca == Placa);
            if(NumPlaca == null)
            {
                result.Message = $"No se encontro la placa con la numeracion {NumPlaca}";
                result.IsError = true;
                result.Data = NumPlaca;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = NumPlaca;
            return Ok(result);
        }
        [HttpGet]
        [Route("MarcaModel")]
        public ActionResult MarcaYModelo(string Marca, string Modelo)
        {
            ResultApi result = new ResultApi();
            var MM = dbContext
                .Taxis
                .FirstOrDefault(x => x.Marca == Marca && x.Modelo == Modelo);
                if(MM == null)
            {
                result.Message = $"No se encontro el taxi con la marca {Marca} y el modelo {Modelo}";
                result.IsError = true;
                result.Data = MM;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = MM;
            return Ok(result);
        }
        [HttpGet]
        [Route("Añotaxi")]
        public ActionResult AñodeTaxi(string Marca)
        {
            ResultApi result = new ResultApi();
            var TaxiMarca = dbContext
                .Taxis
                .FirstOrDefault(x => x.Marca == Marca);
            if(TaxiMarca == null)
            {
                result.Message = $"No se encontro el taxi de la marca {TaxiMarca}";
                result.IsError = true;
                result.Data = TaxiMarca;
                return BadRequest(result);
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = TaxiMarca.Año;
            return Ok(result);
        }
        [HttpGet]
        [Route("TaxiSinChofer")]
        public ActionResult SinChofer()
        {
            ResultApi result = new ResultApi();
            var NoChofer = dbContext
                .Taxis
                .Where(x => !x.Choferes.Any())
                .ToList();
            result.Data = NoChofer;
            result.Message = "Ok";
            return Ok(result);
        }
        [HttpGet]
        [Route("TaxiId")]
        public ActionResult TaxiId(int Id)
        {
            ResultApi result = new ResultApi();
            var Taxi = dbContext
                .Taxis
                .FirstOrDefault(x => x.Id == Id);
            if(Taxi == null)
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

    }
}