
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cine_api.Entities;
using Microsoft.EntityFrameworkCore;


namespace cine_api.Context
{
    public class CineDbContext:DbContext
    {
        public DbSet<Pelicula> Peliculas{get;set;}
        public DbSet<Sala> Salas{get;set;}

        public CineDbContext(DbContextOptions<CineDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constr = "server=localhost;database=Cines;uid=root;pwd=pwd123;port=3306;";
            optionsBuilder.UseMySql(constr, ServerVersion.AutoDetect(constr));

        }
    }

     
    
}