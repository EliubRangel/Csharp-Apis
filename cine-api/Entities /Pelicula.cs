namespace cine_api.Entities
{
    public class Pelicula
    {
       public int Id{get;set;}
        public string Titulo{get;set;}
        public string Descripcion{get;set;}
        public int Calificacion{get;set;}
        public Clasificacion Clasificacion{get;set;}
        public string Director{get;set;}
        public List<Sala>Salas{get;set;}

        public Pelicula(string Titulo, string Descripcion, int Calificacion,string Director)
        {
            this.Titulo=Titulo;
            this.Descripcion=Descripcion;
            this.Calificacion=Calificacion;
            this.Clasificacion=Clasificacion;
            this.Director=Director;
        }
        public override string ToString()
        {
            return $"{Id.ToString().PadRight(10)}{Titulo.ToString().PadRight(20)}{Descripcion.ToString().PadRight(20)}{Calificacion.ToString().PadRight(20)}{Clasificacion.ToString().PadRight(20)}{Director.ToString().PadRight(20)}";
        }

    }
    public enum Clasificacion
    {
        Niños,
        Adolescentes,
        Adultos
    }
}