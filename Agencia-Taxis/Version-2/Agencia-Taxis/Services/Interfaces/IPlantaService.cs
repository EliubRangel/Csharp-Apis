using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;

namespace Agencia_Taxis.Services.Interfaces
{
	public interface IPlantaService
	{
		ResultApi Get();
        ResultApi Nuevaplanta(Planta planta);
        ResultApi ActualizarPlanta(Planta planta);
        ResultApi EliminarPlanta(int Id);
        ResultApi TrasladarTaxi(TrasladarTaxiDto dto);
        ResultApi PlantaCp(string Cp);
        ResultApi PlantaSinTaxis();
        ResultApi PlantaFechas(DateTime FechaInicio, DateTime FechaFin);
        ResultApi DatosPlantas(int Id);
        ResultApi EspaciosDisponibles();
    }
}

