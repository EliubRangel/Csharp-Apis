using System;
namespace Agencia_Taxis.Entities
{
	public class Reportes
	{
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public RazonMulta RazonMulta { get; set; }
        public DateTime Fecha { get; set; }
        public Estatus Estatus { get; set; }
        public Taxis? Taxi { get; set; }
        public int TaxiId { get; set; }
        public int ChoferId { get; set; }
        public Choferes? Chofer { get; set; }

    }
    public enum RazonMulta
    {
        InfeccionVehicular,
        MalaConducta,
        TratosDeshonestos
    }
    public enum Estatus
    {
        Abierto,
        Resuelto,
        Cancelado
    }
}


