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
        public List<Choferes> Choferes { get; set; }


        public Taxis(int Id, string Marca, string Modelo, int Año, string Placas, string NumeroPlaca)
        {
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Año = Año;
            this.Placas = Placas;
            this.NumeroPlaca = NumeroPlaca;
            this.Choferes = new List<Choferes>();
        }


    }
}