using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencia_Taxis.Entities;
using Microsoft.EntityFrameworkCore;
namespace Agencia_Taxis
{
    public class AgenciaDbContext : DbContext
    {
        public DbSet<Choferes> Choferes { get; set; }
        public DbSet<Taxis> Taxis { get; set; }


        public AgenciaDbContext(DbContextOptions<AgenciaDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constr = "server=localhost;database=AgenciaTaxis;uid=root;pwd=pwd123;port=3306;";
            optionsBuilder.UseMySql(constr, ServerVersion.AutoDetect(constr));

        }


    }
}