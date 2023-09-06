namespace Agencia_Taxis.Entities
{
    public class Taxis
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
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