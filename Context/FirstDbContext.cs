using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csharp_Apis;
using Csharp_Apis.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csharp_Apis.Context
{
    public class FirstDbContext : DbContext
    {
        public DbSet<Ventas> Ventas{get;set;}
        public DbSet<Clientes> Clientes{get;set;}
        public FirstDbContext(DbContextOptions<FirstDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constr = "server=localhost;database=CSHARP-APIS;uid=root;pwd=pwd123;port=3306;";
            optionsBuilder.UseMySql(constr, ServerVersion.AutoDetect(constr));

        }

    }
}