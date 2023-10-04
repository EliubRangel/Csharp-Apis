using System.ComponentModel.DataAnnotations.Schema;

namespace Agencia_Taxis.Entities
{
    //[Table("TaxiCDmx")]
    public class Taxis
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        [Column("Año")]
        public int Anio { get; set; }
        public string Placas { get; set; }
        public string NumeroPlaca { get; set; }
        public virtual ICollection<Choferes>? Choferes { get; set; }
        public virtual Planta? Planta{get;set;}
        public virtual ICollection<Reportes>? Reportes{get;set;}

        public Taxis()
        {
            //this.Choferes= new List<Choferes>();
            ////this.Planta = new Planta();
            //this.Reportes = new List<Reportes>();
        }
        
      



    }
}