using Microsoft.AspNetCore.Mvc;
using primeraAPI.DataBase;
using primeraAPI.Models;
using primeraAPI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace primeraAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductsController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("ConsultarProducto")]
        public ActionResult<ProductResponse> ConsultarProducto(int idProducts)
        {
            try
            {
                var product = _productServices.GetProductById(idProducts);

                if (product == null)
                {
                    return NotFound(new { Message = "Producto no encontrado" });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al consultar producto: {ex.Message}" });
            }
        }

        [HttpGet("ConsultarTodosLosProductos")]
        public ActionResult<IEnumerable<ProductResponse>> ConsultarTodosLosProductos()
        {
            try
            {
                var productos = _productServices.GetAllProducts();

                if (productos == null || !productos.Any())
                {
                    return NotFound(new { Message = "No se encontraron productos." });
                }

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al consultar todos los productos: {ex.Message}" });
            }
        }


    }
}


//creo que no sirve
////namespace primeraAPI.Controllers
////{
////    [ApiController]
////    [Route("api")]
////    public class ProductsController : ControllerBase
////    {
////        private readonly ProductServices _productServices;

////        public ProductsController(ProductServices productServices)
////        {
////            _productServices = productServices;
////        }

////        [HttpGet("ConsultarProducto")]
////        public ActionResult<ProductResponse> ConsultarProducto(int idProducts)
////        {
////            try
////            {
////                var product = _productServices.GetProductById(idProducts);

////                if (product == null)
////                {
////                    return NotFound(new { Message = "Producto no encontrado" });
////                }

////                return Ok(product);
////            }
////            catch (Exception ex)
////            {
////                return StatusCode(500, new { Message = $"Error al consultar producto: {ex.Message}" });
////            }
////        }
////    }
////}