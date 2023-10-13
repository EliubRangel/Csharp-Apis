using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agencia_Taxis.Services.Interfaces
{
	public interface IChoferService
	{
        ResultApi AsignarTaxi(AsignarTaxiDto dto);
        ResultApi NuevoChofer(Choferes choferes);
        ResultApi Get();
        ResultApi ActualizarChofer(Choferes choferes);
        ResultApi Eliminar(int id);
        ResultApi MayorEdad();
        ResultApi LicenciaExpirada();
        ResultApi SinTaxis();
        ResultApi ChoferEstatusAbierto();
        ResultApi ChoferId(int Id);
        ResultApi ConTaxi();


    }
}

