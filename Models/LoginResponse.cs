namespace primeraAPI.Models
{
    public class LoginResponse : GeneralResponse
    {
        public new int? IsSuccess { get; set; }
        public new string? Message { get; set; } = "";
        public int? id_user { get; set; }
        public string? email { get; set; } = "";
        public string? Nombre { get; set; } = "";
        public int? Estatus { get; set; }
        public string? token { get; set; } 
    }
}