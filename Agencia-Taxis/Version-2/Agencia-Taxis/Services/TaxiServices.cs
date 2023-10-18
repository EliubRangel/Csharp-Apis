using System;
using Agencia_Taxis.Models;
using Agencia_Taxis.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agencia_Taxis.Services.Interfaces;
using Agencia_Taxis.Entities;

namespace Agencia_Taxis.Services
{
	public class TaxiServices : ITaxiService

	{
        private readonly AgenciaDbContext dbContext;

        public TaxiServices(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        public ResultApi Get()
        {
            ResultApi result = new ResultApi();

            var taxis = dbContext.Taxis.ToList();
            result.Data = taxis;
            result.Message = "ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi NuevoTaxi(Taxis taxis)
        {
            //validar el obj cliente
            ResultApi result = new ResultApi();
            dbContext.Taxis.Add(taxis);
            dbContext.SaveChanges();
            result.Message = "Se agrego el taxi correctamente";
            result.Data = taxis;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi ActualizarTaxi(Taxis taxis)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == taxis.Id);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {taxi.Id}";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
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
                result.StatusCode = 200;
                return result;
            }

        }
    }
}

