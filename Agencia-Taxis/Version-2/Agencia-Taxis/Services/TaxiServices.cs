using System;
using Agencia_Taxis.Models;
using Agencia_Taxis.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agencia_Taxis.Services.Interfaces;

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
    }
}

