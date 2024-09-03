namespace primeraAPI.DTO
{
    public class LoginDTO
    {
        public string? email { get; set; } = "";
        public string? password { get; set; } = "";
    }
    public class RegisterDTO
    {
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string name { get; set; } = "";
    }
    public class ResetPasswordDTO
    {
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string newpassword { get; set; } = "";
    }
}
