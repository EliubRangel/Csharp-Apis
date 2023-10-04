using Agencia_Taxis.Entities;

namespace Agencia_Taxis.models
{
    public class ResultApi
    {
        public string Message {get;set;}
        public bool IsError {get;set;}
        public object Data{get;set;}
    }
}