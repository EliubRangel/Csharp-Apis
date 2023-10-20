using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;

namespace Agencia_Taxis.Services.Interfaces
{
	public interface ITaxiService 
	{
		ResultApi Get();
		ResultApi NuevoTaxi(Taxis taxi);
		ResultApi ActualizarTaxi(Taxis taxis);
		ResultApi EliminarTaxi(int Id);
		ResultApi TaxiPorPlaca(string placa);
		ResultApi MarcaYModelo(string marca, string modelo);
		ResultApi YearTaxi(string marca);
		ResultApi SinChofer();
		ResultApi TaxiId(int id);
		ResultApi MarcaHonda();
		ResultApi InformacionTaxi(int anio);
		ResultApi InformacionReporte();

	}
}

