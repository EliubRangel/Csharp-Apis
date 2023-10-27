using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;

namespace Agencia_Taxis.Services.Interfaces
{
	public interface IReporteService
	{
		ResultApi NuevoReporte(Reportes reportes);
		ResultApi ConsultarReporte(int IdChofer, bool includeAll = false);
		ResultApi ResolverReporte(int Id);
		ResultApi CancelarReporte(int Id);
		ResultApi ReporteId(int Id);
	}
}

