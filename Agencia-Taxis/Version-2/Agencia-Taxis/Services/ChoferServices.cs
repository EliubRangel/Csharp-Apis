using System;
using Agencia_Taxis.DbContexts;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Agencia_Taxis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencia_Taxis.Services

{
	public class ChoferServices : IChoferService
	{
        private readonly AgenciaDbContext dbContext;

        public ChoferServices(AgenciaDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        
	public ResultApi AsignarTaxi(AsignarTaxiDto dto)
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
        public ResultApi NuevoChofer(Choferes choferes)
        {
            //validar el obj cliente
            ResultApi result = new ResultApi();
            int edad = DateTime.Now.Year - choferes.FechaNacimiento.Year;
            if (edad > 18 && edad < 80)
            {
                dbContext.Choferes.Add(choferes);
                dbContext.SaveChanges();
                result.Message = "Se agrego el chofer correctamente";
                result.Data = choferes;
                result.StatusCode = 200;
                return result;
            }
            else
            {
                result.Message = $" Edad del chofer no es permitida";
                result.Data = edad;
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }

        }
        public ResultApi Get()
        {
            ResultApi result = new ResultApi();
            var choferes = dbContext.Choferes
                .Include(x => x.Taxis)
                .ToList();
            result.Data = choferes;
            result.Message = "Ok";
            result.StatusCode = 200;

            return result; ;
        }
        public ResultApi ActualizarChofer(Choferes choferes)
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext.Choferes.FirstOrDefault(x => x.Id == choferes.Id);
            if (chofer == null)
            {
                result.Message = $"No se encontro el chofer con el Id {chofer.Id}";
                result.IsError = true;
                result.StatusCode = 404;
                return result; 
            }
            else
            {
                chofer.Nombre = choferes.Nombre;
                chofer.Apellido = choferes.Apellido;
                chofer.NumeroLicencia = choferes.NumeroLicencia;
                chofer.FechaExpiracion = choferes.FechaExpiracion;
                chofer.FechaNacimiento = choferes.FechaNacimiento;

                dbContext.Update(chofer);
                dbContext.SaveChanges();
                result.Data = chofer;
                result.Message = $"Se modifico el chofer con el Id {chofer.Id} correctamente";
                result.StatusCode = 200;
                return result;
            }
        }
        public ResultApi Eliminar(int Id)
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext.Choferes.FirstOrDefault(x => x.Id == Id);
            if (chofer == null)
            {
                result.Message = $"No se encontro el chofer con el Id{chofer.Id}";
                result.IsError = true;
                result.Data = chofer;
                result.StatusCode = 404;
                return result;
            }
            dbContext.Remove(chofer);
            dbContext.SaveChanges();
            result.Message = $"Se elimino el chofer con el {chofer.Id} correctamente";
            result.Data = chofer;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi MayorEdad()
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext
                .Choferes
                .Where(x => DateTime.Today.AddYears(-50) >= x.FechaNacimiento)
                .ToList();
            result.Data = chofer;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi LicenciaExpirada()
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext
                .Choferes
                .Where(x => DateTime.Today >= x.FechaExpiracion)
                .ToList();
            result.Data = chofer;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }

        public ResultApi SinTaxis()
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext
                .Choferes
                //el metodo any sin paramatros te dice si una
                // coleccion contiene elementos.
                // la negacion con el simbolo ! es una negacion que significa
                //que se incluiran los choferes que no tengan taxis
                .Where(x => !x.Taxis.Any())
                .ToList();
            result.Data = chofer;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi ChoferEstatusAbierto()
        {
            ResultApi result = new ResultApi();
            var reportes = dbContext
                .Reportes
                .Where(x => x.Estatus == Estatus.Abierto)
                .ToList();
            result.Data = reportes;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }
        public ResultApi ChoferId(int Id)
        {
            ResultApi result = new ResultApi();
            var ChoferId = dbContext
                .Choferes
                .FirstOrDefault(x => x.Id == Id);
            if (ChoferId == null)
            {
                result.Message = $"No se encontro chofer con el Id {ChoferId}";
                result.IsError = true;
                result.Data = ChoferId;
                result.StatusCode = 400;
                return result;
            }
            dbContext.SaveChanges();
            result.Message = "Ok";
            result.Data = ChoferId;
            result.StatusCode = 200;
            return result;
        }
        public ResultApi ConTaxi()
        {
            ResultApi result = new ResultApi();
            var chofer = dbContext
                .Choferes
                .Where(x => x.Taxis.Any())
                .Select(x => x.Nombre)
                .ToList();
            if (chofer == null)
            {
                result.Message = "No se encontraron choferes con taxis";
                result.IsError = true;
                result.StatusCode = 400;
                return result;
            }
            result.Data = chofer;
            result.Message = "Ok";
            result.StatusCode = 200;
            return result;
        }

    }
}