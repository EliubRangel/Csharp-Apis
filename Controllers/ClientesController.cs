using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csharp_Apis.Context;
using Microsoft.AspNetCore.Mvc;

namespace Csharp_Apis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController:Controller
    {
        private readonly FirstDbContext dbContext;

        public ClientesController(FirstDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var clientes = dbContext.Clientes.ToList();
            return Ok(clientes);
        }

    }
}