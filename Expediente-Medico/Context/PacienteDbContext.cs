using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Expediente_Medico.Entities
{
    public class PacienteDbContext:DbContext
    {
        public DbSet<Paciente> pacientes{get;set;}
        public DbSet<Consulta> Consultas{get;set;}

        public PacienteDbContext(DbContextOptions<PacienteDbContext>options):base(options)
        {}

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constr = "server=localhost;database=Pacientes;uid=root;pwd=pwd123;port=3306;";
            optionsBuilder.UseMySql(constr, ServerVersion.AutoDetect(constr));

        }
    }
}