namespace Agencia_Taxis.Entities
{
    public class Choferes
    {
        public Choferes(int id, string nombre, string apellido, string numeroLicencia, DateTime fechaExpiracion, DateTime fechaNacimiento)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.NumeroLicencia = numeroLicencia;
            this.FechaExpiracion = fechaExpiracion;
            this.FechaNacimiento = fechaNacimiento;

        }
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