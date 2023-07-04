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
        public List<Taxis> Taxis { get; set; }


        public Choferes(int Id, string Nombre, string Apellido, string NumeroLicencia, DateTime FechaExpiracion, DateTime FechaNacimiento)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.NumeroLicencia = NumeroLicencia;
            this.FechaExpiracion = FechaExpiracion;
            this.FechaNacimiento = FechaNacimiento;
            this.Taxis = new List<Taxis>();
        }
    }
}