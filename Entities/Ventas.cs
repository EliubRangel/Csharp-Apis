namespace Csharp_Apis.Entities
{
    public class Ventas
    {
        public int Id{get;set;}
        public string Articulo{get;set;}
        public string Precio{get;set;}
        public int Cantidad{get;set;}
        public int Total{get;set;}

        public Ventas (int Id, string Articulo, string Precio, int Cantidad, int Total)
    {
        this.Id=Id;
        this.Articulo=Articulo;
        this.Precio=Precio;
        this.Cantidad=Cantidad;
        this.Total=Total;
    }

    public override string ToString()
        {
            return $"{Id.ToString().PadRight(10)}{Articulo.ToString().PadRight(20)}{Precio.ToString().PadRight(20)}{Cantidad.ToString().PadRight(10)}{Total.ToString().PadRight(10)}";
        }
    }

    
}