using Microsoft.Data.SqlClient;
//using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using primeraAPI.DTO;
using primeraAPI.Models;
using System.Data;

namespace primeraAPI.DataBase
{
    public class AccessDB(IConfiguration configuration)
    {
        private readonly string _dataBase = configuration["ConnectionStrings:DataBase"]!;

        public LoginResponse Login(Models.LoginRequest loginRequest)
        {
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new SqlCommand("dbo.Access", connection);
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@method", "login");
                        if (loginRequest.email == null)
                        {
                            command.Parameters.AddWithValue("@email", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@email", loginRequest.email);
                        }

                        if (loginRequest.password == null)
                        {
                            command.Parameters.AddWithValue("@password", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@password", loginRequest.password);
                        }
                   
                        var result = command.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(result))
                        {
                            var jsonObject = JObject.Parse(result);

                            if (jsonObject != null)
                            {
                                return new LoginResponse
                                {
                                    IsSuccess = jsonObject["IsSuccess"]?.Value<int>() ?? 0,
                                    Message = jsonObject["message"]?.Value<string>() ?? "",
                                    id_user = jsonObject["id_user"]?.Value<int>() ?? 0,
                                    email = jsonObject["email"]?.Value<string>() ?? "",
                                    Nombre = jsonObject["Nombre"]?.Value<string>() ?? "",
                                    Estatus = jsonObject["Estatus"]?.Value<int>() ?? 0
                                };
                            }
                        }

                        return new LoginResponse { Message = "Unexpected error occurred." };
                    }
                }
                catch (Exception ex)
                {
                    return new LoginResponse { Message = ex.Message };
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------
        // Método de registro
        public LoginResponse Register(RegisterDTO registerDTO)
        {
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("dbo.Access", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@method", "register");
                        command.Parameters.AddWithValue("@email", (object)registerDTO.email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@password", (object)registerDTO.password ?? DBNull.Value);
                        command.Parameters.AddWithValue("@name", (object)registerDTO.name ?? DBNull.Value);

                        var result = command.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(result))
                        {
                            var jsonObject = JObject.Parse(result);

                            if (jsonObject != null)
                            {
                                return new LoginResponse
                                {
                                    IsSuccess = jsonObject["IsSuccess"]?.Value<int>() ?? 0,
                                    Message = jsonObject["message"]?.Value<string>() ?? ""
                                };
                            }
                        }

                        return new LoginResponse { Message = "Unexpected error occurred." };
                    }
                }
                catch (Exception ex)
                {
                    return new LoginResponse { Message = ex.Message };
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------
        public LoginResponse ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            using SqlConnection connection = new SqlConnection(_dataBase);
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("dbo.Access", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@method", "change_password");
                        command.Parameters.AddWithValue("@email", (object)resetPasswordDTO.email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@password", (object)resetPasswordDTO.password ?? DBNull.Value); 
                        command.Parameters.AddWithValue("@new_password", (object)resetPasswordDTO.newpassword ?? DBNull.Value);

                        // Ejecutar el procedimiento almacenado y obtener el resultado
                        var result = command.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(result))
                        {
                            var jsonObject = JObject.Parse(result);

                            if (jsonObject != null)
                            {
                                return new LoginResponse
                                {
                                    IsSuccess = jsonObject["IsSuccess"]?.Value<int>() ?? 0,
                                    Message = jsonObject["message"]?.Value<string>() ?? ""
                                };
                            }
                        }
                        return new LoginResponse { Message = "Unexpected error occurred." };
                    }
                }
                catch (Exception ex)
                {
                    return new LoginResponse { Message = ex.Message };
                }
            }
        }
    }
}
