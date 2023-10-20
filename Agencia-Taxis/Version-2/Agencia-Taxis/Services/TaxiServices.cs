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
        public ResultApi EliminarTaxi(int Id)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == Id);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id{taxi.Id}";
                result.IsError = true;
                result.Data = taxi;
                result.StatusCode = 404;
                return result;
            }
            dbContext.Remove(taxi);
            dbContext.SaveChanges();
            result.Message = $"Se elimino taxi con el Id {taxi.Id} correctamente";
            result.Data = taxi;
            result.StatusCode = 200;
            return result;

        }
        public ResultApi TaxiPorPlaca(string placa)
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
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi MarcaYModelo(string marca, string modelo)
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
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi YearTaxi(string Marca)
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
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = taxi.Anio;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi SinChofer()
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .Where(x => !x.Choferes.Any())
                .ToList();
            result.Data = taxi;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi TaxiId(int Id)
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
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Data = Taxi;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi MarcaHonda()
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext
                .Taxis
                .Where(x => x.Marca == "honda")
                .Select(x => x.NumeroPlaca)
                .ToList();
            if (taxi == null)
            {
                result.Message = "No se encontraron taxis de de marca Honda ";
                result.Data = taxi;
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Data = taxi;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;

        }
        public ResultApi InformacionTaxi(int Anio)
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
                }).ToList();
            if (informacion == null)
            {
                result.Message = $"No se encontraron vehiculos con el anio {Anio}";
                result.Data = informacion;
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Data = informacion;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi InformacionReporte()
        {
            ResultApi result = new ResultApi();
            var informacion = dbContext
                .Reportes
                .Where(r => r.Estatus == Estatus.Abierto)
                .Select(r => new InformacionTaxiDto
                {
                    NombreChofer = $"{r.Chofer.Nombre} {r.Chofer.Apellido}",
                    RazonMulta = r.RazonMulta.ToString(),
                    DescripcionMulta = r.Descripcion,
                    Anio = r.Taxi.Anio,
                    Marca = r.Taxi.Marca
                })
                .ToList();
            if (informacion == null)
            {
                result.Message = "No se encontraron taxis con reportes abiertos";
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Data = informacion;
            result.Message = "ok";
            result.StatusCode = 200;
            return result;
        }
    }
}

