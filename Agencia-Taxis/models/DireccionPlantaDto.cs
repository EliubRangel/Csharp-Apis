using System;
namespace Agencia_Taxis.models
{
	// DTO significa Data Transfer Object
	// Objeto de transferencia de datos
	// Esta es una clase para guardar/transferir los datos de la consulta
	public class DireccionPlantaDto
	{
		public string Direccion { get; set; }
		public string ZipCode { get; set; }
		public string Colonia { get; set; }
	}
}

