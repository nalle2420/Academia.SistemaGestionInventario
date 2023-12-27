using Academia.SistemaGestionInventario.WApi._Features.Productos;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _serviceProducto;


        public ProductoController(ProductoService serviceProducto)
        {
            _serviceProducto = serviceProducto;
        }

        [HttpGet]
        [Route("productos")]
        public IActionResult ObtenerProductos()
        {

            var resultado = _serviceProducto.BuscarProductos();
            return Ok(resultado);

        }

    }
}
