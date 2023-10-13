using System;
using Agencia_Taxis.Services;
using Agencia_Taxis.Entities;
using Microsoft.AspNetCore.Mvc;
using Agencia_Taxis.Models;
using Microsoft.EntityFrameworkCore;
using Agencia_Taxis.Services.Interfaces;

namespace Agencia_Taxis.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class TaxiController : Controller
	{
        private readonly ITaxiService _taxiServices;
        public TaxiController(ITaxiService taxiService)
        {
            this._taxiServices = taxiService;
        }
        [HttpGet]
        public ActionResult get()
        {
            var result = _taxiServices.Get();
            return StatusCode(result.StatusCode, result);
        }

    }
}

