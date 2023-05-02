using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Csharp_Apis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet]
        [Route("sum")]
        public ActionResult GetSuma(int Numero1, int Numero2)
        {
            MathResponse Result = new MathResponse()
            {
                Numero1=Numero1,
                Numero2=Numero2,
                Operation="Sum",
                Result = Numero1 + Numero2
            };
            return Ok (Result);
        }
        [HttpGet]
        [Route("substraction")]

        public ActionResult GetResta(int Numero1, int Numero2)
        {
            MathResponse Result = new MathResponse()
            {
                Numero1=Numero1,
                Numero2=Numero2,
                Operation="substraction",
                Result = Numero1 - Numero2
            };
            return Ok (Result);
        }
        

        
    }
}