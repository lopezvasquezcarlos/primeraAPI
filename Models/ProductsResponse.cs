namespace primeraAPI.Models
{
    public class ProductResponse
    {
        public int IdProducts { get; set; }
        public double Precio { get; set; } 
        public string Categoria { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Stock { get; set; } 
        public string Codigo { get; set; } = "";
        public string ImagenURL { get; set; } = "";
    }
}


//public string ImagenURL { get; set; } = "";