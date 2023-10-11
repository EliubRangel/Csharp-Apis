using System;
using System.Numerics;
using Agencia_Taxis.DbContexts;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Agencia_Taxis.Services
{
	public class PlantaServices
	{
        private readonly AgenciaDbContext dbContext;

        public PlantaServices(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        public ResultApi Get()
        {
            ResultApi result = new ResultApi();

            var Planta = dbContext.Planta.Include(x => x.Taxis).ToList();

            result.Data = Planta;
            result.Message = "ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi Nuevaplanta(Planta planta)
        {

            ResultApi result = new ResultApi();
            dbContext.Planta.Add(planta);
            dbContext.SaveChanges();
            result.Message = "La planta se agrego correctamente";
            result.Data = planta;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi ActualizarPlanta(Planta planta)
        {
            ResultApi result = new ResultApi();
            //pasos para actualizar un registro en ef:
            //1: consultar el registro a modificar. se necesita consultar para que ef mantenga un la relacion.


            var Pta = dbContext.Planta.FirstOrDefault(x => x.Id == planta.Id);
            if (Pta == null)
            {
                result.Message = $"No se encontro la planta con el Id {Pta.Id}";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
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
                result.StatusCode = 200;
                return result;
            }

        }
        public ResultApi EliminarPlanta(int Id)
        {
            ResultApi result = new ResultApi();
            var planta = dbContext.Planta.FirstOrDefault(x => x.Id == Id);
            if (planta == null)
            {
                result.Message = $"No se encontro la planta con el Id{planta.Id}";
                result.IsError = true;
                result.Data = planta;
                result.StatusCode = 404;
                return result;
            }
            dbContext.Remove(planta);
            dbContext.SaveChanges();
            result.Message = $"Se elimino el chofer con el {planta.Id} correctamente";
            result.Data = planta;
            result.StatusCode = 200;
            return result;

        }
        public ResultApi TrasladarTaxi(TrasladarTaxiDto dto)
        {
            ResultApi result = new ResultApi();
            var taxi = dbContext.Taxis.Include(x => x.Planta).FirstOrDefault(x => x.Id == dto.IdTaxi);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {dto.IdTaxi}";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }

            var Planta = dbContext.Planta
                .Include(x => x.Taxis)
                .FirstOrDefault(x => x.Id == dto.IdPlanta);
            if (Planta == null)
            {
                result.Message = $"No se encontro la Planta con el id {dto.IdPlanta}";
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            if (Planta.EspaciosDisponibles >= 1)
            {
                Planta.Taxis.Add(taxi);
                Planta.EspaciosDisponibles = Planta.EspaciosDisponibles - 1;
                if (taxi.Planta != null)
                    taxi.Planta.EspaciosDisponibles = taxi.Planta.EspaciosDisponibles + 1;
                dbContext.Update(Planta);
                dbContext.SaveChanges();
            }
            else
            {
                result.Message = $"No se encontro espacio disponible en la planta";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }
            result.Message = $"Se traslado el taxi {dto.IdTaxi} correctamente";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi PlantaCp(string Cp)
        {
            ResultApi result = new ResultApi();
            var planta = dbContext
                .Planta
                .FirstOrDefault(x => x.CodigoPostal == Cp);
            if (planta == null)
            {
                result.Message = $"No se encontro la planta con el Cp {Cp}";
                result.IsError = true;
                result.Data = planta;
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = planta;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi PlantaSinTaxis()
        {
            ResultApi result = new ResultApi();
            var planta = dbContext
                .Planta
                .Where(x => !x.Taxis.Any())
                .ToList();
            result.Data = planta;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi PlantaFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            ResultApi result = new ResultApi();
            var planta = dbContext
                .Planta
                .FirstOrDefault(x => x.FechaApertura >= FechaInicio && x.FechaApertura <= FechaFin);
            if (planta == null)
            {
                result.Message = $"No se encontro planta que cumpla con este rango de fechas";
                result.Data = planta;
                result.IsError = true;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = planta;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi DatosPlantas(int Id)
        {
            ResultApi result = new ResultApi();
            var direccion = dbContext
                .Planta
                .Where(x => x.Id == Id)
                .Select((Planta x) => new DireccionPlantaDto
                {
                    Colonia = x.Colonia,
                    ZipCode = x.CodigoPostal,
                    Direccion = x.Direccion
                })
                .FirstOrDefault();
            if (direccion == null)
            {
                result.Message = $"No se encontro planta con el Id {Id}";
                result.Data = direccion;
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Message = "Ok";
            result.Data = direccion;
            result.StatusCode = 200;
            return result;


        }
        public ResultApi EspaciosDisponibles()
        {
            ResultApi result = new ResultApi();
            var planta = dbContext
                .Planta
                .Where(x => x.EspaciosDisponibles >= 1)
                .Select(x => x.EspaciosDisponibles);
            if (EspaciosDisponibles == null)
            {
                result.Message = "No se encontraron espacios disponibles";
                result.Data = planta;
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Data = planta;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }

    }
}

