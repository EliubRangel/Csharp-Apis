using System;
namespace Agencia_Taxis.Entities
{
	public class Planta
	{

        public int Id { get; set; }
        public string Encargado { get; set; }
        public string CodigoPostal { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public int NumeroAfiliacion { get; set; }
        public DateTime FechaApertura { get; set; }
        public int EspaciosDisponibles { get; set; }
        public int EspaciosTotales { get; set; }
        public ICollection<Taxis> Taxis { get; set; }

        public Planta()
        {
            this.Taxis = new List<Taxis>();
        }
    }
}

