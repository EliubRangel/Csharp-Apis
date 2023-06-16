using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cine_api.Entities;
using cine_api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace cine_api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CineController : Controller
    {
        private readonly CineDbContext dbContext;

        public CineController(CineDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        [HttpPost]
        public ActionResult NuevaPelicula(Pelicula pelicula)
        {
            //validar el obj cliente
            dbContext.Peliculas.Add(pelicula);
            dbContext.SaveChanges();
            return Ok(pelicula);
        }
        [HttpGet]
        public ActionResult Get()
        {
            var clientes = dbContext.Peliculas.ToList();
            return Ok(clientes);
        }
        [HttpPut]
        public ActionResult ActualizarPelicula(Pelicula pelicula)
        {
            var cte = dbContext.Peliculas.FirstOrDefault(x => x.Id == pelicula.Id);
            cte.Titulo = pelicula.Titulo;
            cte.Descripcion = pelicula.Descripcion;
            cte.Calificacion = pelicula.Calificacion;
            cte.Director = pelicula.Director;

            dbContext.Update(cte);
            dbContext.SaveChanges();
            return Ok(cte);
        }
        [HttpDelete]
        public ActionResult EliminarPelicula(int Id)
        {
            var cte =dbContext.Peliculas.FirstOrDefault(x=> x.Id == Id);
            if(cte == null)
            {
               return NotFound("No se encontro la pelicula con el Id");
            }
            dbContext.Remove(cte);
            dbContext.SaveChanges();
            return Ok(cte);
            
        }
        

    }
}