using primeraAPI.DataBase;
using primeraAPI.Models;
namespace primeraAPI.Services
{
    public class AccessServices
    {
        private readonly AccessDB _accessDB;
        public AccessServices(AccessDB accessDB)
        {
            _accessDB = accessDB;
        }
        public LoginResponse Login(LoginRequest request)
        {
            return _accessDB.Login(request);
        }
    }
}
