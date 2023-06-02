using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csharp_Apis.Context;
using Csharp_Apis.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Csharp_Apis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
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

        [HttpPost]
        public ActionResult NuevoCliente(Clientes cliente)
        {
            //validar el obj cliente
            dbContext.Clientes.Add(cliente);
            dbContext.SaveChanges();
            return Ok(cliente);
        }

        [HttpPut]
        public ActionResult ModificarCliente(Clientes cliente)
        {
            var cte = dbContext.Clientes.FirstOrDefault(x => x.Id == cliente.Id);
            cte.Nombre = cliente.Nombre;
            cte.Apellido = cliente.Apellido;
            cte.FechaNacimiento = cliente.FechaNacimiento;
            cte.Factura = cliente.Factura;
            cte.Telefono = cliente.Telefono;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            return Ok(cte);
        }
        [HttpDelete]
        public ActionResult EliminarCliente(int Id)
        {
            var cte =dbContext.Clientes.FirstOrDefault(x=> x.Id == Id);
            if(cte == null)
            {
               return NotFound("No se encontro el cliente con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            return Ok(cte);
            
        }
    }
}