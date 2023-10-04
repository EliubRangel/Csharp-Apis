using System;
namespace Agencia_Taxis.Models
{
    public class ResultApi
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object Data { get; set; }
    }
}

