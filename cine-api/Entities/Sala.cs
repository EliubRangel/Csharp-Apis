namespace cine_api.Entities
{
    public class Sala
    {
        public int Id{get;set;}
        public int NumeroSala{ get; set; }
        public int CantidadAsientos{ get; set; }
        public Tiposala Tiposala{get;set;}
        public List<Pelicula>Peliculas{get;set;}

        public Sala(int NumeroSala, int CantidadAsientos, Tiposala Tiposala)
        {
            this.NumeroSala=NumeroSala;
            this.CantidadAsientos=CantidadAsientos;
            this.Tiposala=Tiposala;
            this.Peliculas = new List<Pelicula>();
        }


    }
    public enum Tiposala
    {
        Tradicional,
        Vip,
        Tridimensional
    }
}