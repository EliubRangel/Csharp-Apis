namespace Csharp_Apis.Entities
{
    public class Clientes
    {
     public int Id{get;set;}
     public string Nombre{get;set;}
     public string Apellido{get;set;}
     public DateTime FechaNacimiento{get;set;}
     public string Telefono{get;set;}   
     public bool Factura{get;set;}
     public List<Ventas> Ventas{get;set;}

     public Clientes (int Id, string Nombre, string Apellido, DateTime FechaNacimiento, string Telefono, bool Factura)
     {
        this.Id=Id;
        this.Nombre=Nombre;
        this.Apellido=Apellido;
        this.FechaNacimiento=FechaNacimiento;
        this.Telefono=Telefono;
        this.Factura=Factura;

     }
     public override string ToString()
        {
            return $"{Id.ToString().PadRight(10)}{Nombre.ToString().PadRight(20)}{Apellido.ToString().PadRight(20)}{FechaNacimiento.ToLongTimeString().PadRight(20)}{Telefono.ToString().PadRight(20)}{Factura.ToString().PadRight(20)}";
        }
    }
}