﻿using System;
using Agencia_Taxis.Entities;
using Agencia_Taxis.Models;
using Agencia_Taxis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencia_Taxis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChoferesController : Controller
    {
        private readonly ChoferServices _choferService;

        public ChoferesController(ChoferServices choferService)
        {
            this._choferService = choferService;
        }

        [HttpPost]
        [Route("taxi")]
        public ActionResult AsignarTaxi(AsignarTaxiDto dto)
        {
            var result = _choferService.AsiganrTaxi(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("newdriver")]
        public ActionResult NuevoChofer(Choferes choferes)
        {
            //5929/choferes/newdriver
            var result = _choferService.NuevoChofer(choferes);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public ActionResult get()
        {
            var result = _choferService.Get();
            return StatusCode(result.StatusCode, result);
        }

    }
}

