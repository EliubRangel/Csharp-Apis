namespace Agencia_Taxis.Entities
{
    public class Choferes
    {
       
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroLicencia { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public ICollection<Taxis> Taxis { get; set; }

        public Choferes()
        {
            this.Taxis= new List<Taxis>(); 
        }



    }
}