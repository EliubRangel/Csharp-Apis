using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Expediente_Medico.Entities;

namespace Expediente_Medico.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class PacienteController : Controller
    {
        private readonly PacienteDbContext dbContext;

        public PacienteController(PacienteDbContext DbContext)
        {
            this.dbContext = DbContext;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var Paciente = dbContext.Consultas.ToList();
            return Ok(Paciente);
        }

        [HttpPost]
        public ActionResult NuevoPaciente(Paciente paciente)
        {
            //validar el obj cliente
            dbContext.pacientes.Add(paciente);
            dbContext.SaveChanges();
            return Ok(paciente);
        }
        [HttpPut]
         public ActionResult ActualizarPaciente(Paciente paciente)
        {
            var cte = dbContext.pacientes.FirstOrDefault(x => x.Id == paciente.Id);
            cte.Nombre = paciente.Nombre;
            cte.Apellido = paciente.Apellido;
            cte.FechaNacimiento = paciente.FechaNacimiento;
            cte.Diabetes = paciente.Diabetes;
            cte.Hipertension = paciente.Hipertension;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            return Ok(cte);
        }
        [HttpDelete]
        public ActionResult EliminarPaciente(int Id)
        {
            var cte =dbContext.pacientes.FirstOrDefault(x=> x.Id == Id);
            if(cte == null)
            {
               return NotFound("No se encontro el paciente con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            return Ok(cte);
            
        }

    }
}