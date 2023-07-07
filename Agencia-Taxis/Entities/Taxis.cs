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
        public ICollection<Choferes> Choferes { get; set; }



    }
}