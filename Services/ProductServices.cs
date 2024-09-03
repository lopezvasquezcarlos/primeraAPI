using System;
using primeraAPI.DataBase;
using primeraAPI.Models;



namespace primeraAPI.Services
{
    public class ProductServices
    {
        private readonly ProductAccessDB _productAccessDB;

        public ProductServices(ProductAccessDB productAccessDB)
        {
            _productAccessDB = productAccessDB;
        }

        public ProductResponse GetProductById(int idProducts)
        {
            return _productAccessDB.ConsultarProducto(idProducts);
        }

        public List<ProductResponse> GetAllProducts()
        {
            return _productAccessDB.ConsultarTodosLosProductos();
        }

    }
}

