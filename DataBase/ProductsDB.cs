using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using primeraAPI.Models;
using Microsoft.Data.SqlClient;



namespace primeraAPI.DataBase
{
    public class ProductAccessDB
    {
        private readonly string _dataBase;

        public ProductAccessDB(IConfiguration configuration)
        {
            _dataBase = configuration["ConnectionStrings:DataBase"]!;
        }

        public ProductResponse? ConsultarProducto(int idProducts)
        {
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new SqlCommand("dbo.ConsultarProductos", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_products", idProducts);

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new ProductResponse
                        {
                            IdProducts = reader.GetInt32(reader.GetOrdinal("id_products")),
                            Precio = reader.GetDouble(reader.GetOrdinal("Precio")),
                            Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                            Codigo = reader.GetString(reader.GetOrdinal("codigo")),
                            //ImagenURL = reader.IsDBNull(reader.GetOrdinal("ImagenURL")) ? null : reader.GetSqlBytes(reader.GetOrdinal("ImagenURL")).Value

                            ImagenURL = reader.GetString(reader.GetOrdinal("ImagenURL"))
                        };
                    }
                    return null; // No se encontraron productos
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    throw new Exception($"Error al consultar producto: {ex.Message}");
                }
            }
        }

        public List<ProductResponse> ConsultarTodosLosProductos()
        {
            using (SqlConnection connection = new SqlConnection(_dataBase))
            {
                try
                {
                    connection.Open();

                    using SqlCommand command = new SqlCommand("dbo.ConsultarProductos", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_products", DBNull.Value);

                    using SqlDataReader reader = command.ExecuteReader();
                    List<ProductResponse> products = new List<ProductResponse>();

                    while (reader.Read())
                    {
                        products.Add(new ProductResponse
                        {
                            IdProducts = reader.GetInt32(reader.GetOrdinal("id_products")),
                            Precio = reader.GetDouble(reader.GetOrdinal("Precio")),
                            Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                            Codigo = reader.GetString(reader.GetOrdinal("codigo")),
                            ImagenURL = reader.GetString(reader.GetOrdinal("ImagenURL"))
                        });
                    }

                    return products;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al consultar productos: {ex.Message}");
                }
            }
        }

    }
}
//namespace primeraAPI.DataBase
//{
//    public class ProductAccessDB(IConfiguration configuration)
//    {
//        private readonly string _dataBase = configuration["ConnectionStrings:DataBase"]!;
//        public ProductResponse ConsultarProducto(int idProducts)
//        {
//            using (SqlConnection connection = new SqlConnection(_dataBase))
//            {
//                try
//                {
//                    connection.Open();

//                    using SqlCommand command = new SqlCommand("dbo.ConsultarProductos", connection);
//                    command.CommandType = CommandType.StoredProcedure;

//                    command.Parameters.AddWithValue("@id_products", idProducts);
//                    command.CommandType = CommandType.StoredProcedure;

//                    using SqlDataReader reader = command.ExecuteReader();
//                    if (reader.Read())
//                    {
//                        return new ProductResponse
//                        {
//                            IdProducts = reader.GetInt32(reader.GetOrdinal("id_products")),
//                            Precio = reader.GetDouble(reader.GetOrdinal("Precio")), 
//                            Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
//                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
//                            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
//                            Codigo = reader.GetString(reader.GetOrdinal("codigo")),
//                            ImagenURL = reader.GetString(reader.GetOrdinal("ImagenURL"))
//                        };
//                    }
//                    return null; // No se encontraron productos
//                }
//                catch (Exception ex)
//                {
//                    // Manejo de errores
//                    throw new Exception($"Error al consultar producto: {ex.Message}");
//                }
//            }
//        }
//    }

//}
