namespace Expediente_Medico.Entities
{
    public class Consulta
    {
        public int Id{get;set;}
        public DateTime FechaConsulta{get;set;}
        public string Descripcion{get;set;}
        public string Diagnostico{get;set;}
        public string Receta{get;set;}

        public Consulta(DateTime FechaConsulta, string Descripcion,string Diagnostico, string Receta)
        {
            this.FechaConsulta= FechaConsulta;
            this.Diagnostico= Diagnostico;
            this.Descripcion= Descripcion;
            this.Receta= Receta;
        }
    }
}