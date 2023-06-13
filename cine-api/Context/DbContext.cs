
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cine_api.Entities;
using Microsoft.EntityFrameworkCore;
namespace cine_api.Context
{
    public class context : DbContext
    {
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Sala> Salas { get; set; }
    }
    
}