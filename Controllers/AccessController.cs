using Microsoft.AspNetCore.Mvc;
using primeraAPI.DataBase;
using primeraAPI.DTO;
using primeraAPI.Models;
using primeraAPI.Services;

namespace primeraAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AccessController : ControllerBase
    {
        private readonly AccessDB _accessDB;
        private readonly TokenService _tokenService;

        public AccessController(AccessDB accessDB, TokenService tokenService)
        {
            _accessDB = accessDB;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponse> Login([FromBody] Models.LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(new LoginResponse { IsSuccess = 0, Message = "Invalid request" });
            }

            var response = _accessDB.Login(loginRequest);

            // Depuración: Imprimir el valor de IsSuccess
            Console.WriteLine($"IsSuccess Value: {response.IsSuccess}");

            if (response.IsSuccess == 1)
            {
                var userDto = new TokenDTO
                {
                    id_user = response.id_user.ToString(),
                    Nombre = response.Nombre,
                    TipoDeUsuario = "Standard" // Ajusta según tu lógica de negocio
                };

                var token = _tokenService.GenerateToken(userDto);

                response.token = token.AccessToken;
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpPost]
        [Route("register")]
        public ActionResult<LoginResponse> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = _accessDB.Register(registerDTO);

            // Depuración: Imprimir el valor de IsSuccess
            Console.WriteLine($"IsSuccess Value: {response.IsSuccess}");

            if (response.IsSuccess == 1)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [Route("reset_password")]
        public ActionResult<LoginResponse> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var response = _accessDB.ResetPassword(resetPasswordDTO);

            // Depuración: Imprimir el valor de IsSuccess
            Console.WriteLine($"IsSuccess Value: {response.IsSuccess}");

            if (response.IsSuccess == 1)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

