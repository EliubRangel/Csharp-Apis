namespace Expediente_Medico.Entities
{

    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Diabetes { get; set; }
        public bool Hipertension { get; set; }
        public List<Consulta> Consultas { get; set; }

        public Paciente()
        {
            
        }

        public Paciente(string Nombre, string Apellido, DateTime FechaNacimento, bool Diabetes, bool Hipertension)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.FechaNacimiento = FechaNacimento;
            this.Diabetes = Diabetes;
            this.Hipertension = Hipertension;
            this.Consultas = new List<Consulta>();
        }

    }
}