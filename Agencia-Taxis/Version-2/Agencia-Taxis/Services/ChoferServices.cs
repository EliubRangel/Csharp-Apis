using System;
using Agencia_Taxis.DbContexts;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Microsoft.EntityFrameworkCore;

namespace Agencia_Taxis.Services

{
	public class ChoferServices 
	{
        private readonly AgenciaDbContext dbContext;

        public ChoferServices(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        
	public ResultApi AsiganrTaxi(AsignarTaxiDto dto)
        {
            ResultApi result = new ResultApi();

            var taxi = dbContext.Taxis.FirstOrDefault(x => x.Id == dto.IdTaxi);
            if (taxi == null)
            {
                result.Message = $"No se encontro el taxi con el Id {dto.IdTaxi}";
                result.IsError = true;
                result.StatusCode = 404;
                return result;

            }

            var chofer = dbContext.Choferes
                .Include(x => x.Taxis)
                .FirstOrDefault(x => x.Id == dto.IdChofer);
            if (chofer == null)
            {
                result.Message = $"No se encontro el chofer con el Id {dto.IdChofer}";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }
            var rep = dbContext.Reportes
               .FirstOrDefault(x => x.Estatus == Estatus.Abierto && x.ChoferId == dto.IdChofer);
            if (rep != null)
            {
                result.Message = $"El chofer con el Id {dto.IdChofer} tiene reportes abiertos";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }
            // El metodo any se encarga de decirte si existe algun elemento que cumpla
            // con una condicion dada. Regresa un true si se cumple la condicion en
            // al menos un elemento y false si no se cumple para ninguno
            var existenReportes = dbContext
            .Reportes
                .Any(x => x.Estatus == Estatus.Abierto && x.ChoferId == dto.IdChofer);
            if (existenReportes)
            {
                result.Message = $"El chofer con el Id {dto.IdChofer} tiene reportes abiertos";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }

            if (chofer.Taxis.Count < 2)
            {

                chofer.Taxis.Add(taxi);
                dbContext.SaveChanges();
            }
            else
            {
                result.Message = $"El chofer con el Id {dto.IdChofer} ya cuenta con 2 taxis";
                result.IsError = true;
                result.StatusCode = 404;
                return result;
            }

            result.Message = "Se asigno el taxi correctamente";
            result.StatusCode = 200;
            return result;
        }
	}
}