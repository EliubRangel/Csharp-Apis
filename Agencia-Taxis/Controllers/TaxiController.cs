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
            dbContext.Taxis.Add(taxis);
            dbContext.SaveChanges();
            return Ok(taxis);
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
            var cte = dbContext.Taxis.FirstOrDefault(x => x.Id == taxis.Id);
            cte.Marca = taxis.Marca;
            cte.Modelo = taxis.Modelo;
            cte.Año = taxis.Año;
            cte.Placas = taxis.Placas;
            cte.NumeroPlaca = taxis.NumeroPlaca;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            return Ok(cte);
        }
        [HttpDelete]
        public ActionResult EliminarTaxi(int Id)
        {
            var cte = dbContext.Taxis.FirstOrDefault(x => x.Id == Id);
            if (cte == null)
            {
                return NotFound("No se encontro el taxi con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            return Ok(cte);

        }

    }
}