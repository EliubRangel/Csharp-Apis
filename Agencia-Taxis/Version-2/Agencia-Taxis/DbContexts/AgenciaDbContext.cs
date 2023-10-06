using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencia_Taxis.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agencia_Taxis.DbContexts
{
	public class AgenciaDbContext : DbContext
	{

        public DbSet<Choferes> Choferes { get; set; }
        public DbSet<Taxis> Taxis { get; set; }
        public DbSet<Planta> Planta { get; set; }
        public DbSet<Reportes> Reportes { get; set; }


        public AgenciaDbContext(DbContextOptions<AgenciaDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constr = "server=localhost;database=AgenciaTaxis;uid=root;pwd=pwd123;port=3306;";
            optionsBuilder.UseMySql(constr, ServerVersion.AutoDetect(constr));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Choferes>()
                .HasMany(x => x.Taxis)
                .WithMany(x => x.Choferes);
            builder.Entity<Taxis>()
                .HasMany(x => x.Choferes)
                .WithMany(x => x.Taxis);
        }
    }
}

