using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoLoteController : ControllerBase
    {
        private readonly IProductoLoteService _serviceLote;

        public ProductoLoteController(IProductoLoteService serviceLote)
        {
            _serviceLote = serviceLote;
        }


        [HttpGet]
        [Route("LotesPorProducto")]
        public IActionResult ObtenerLotesPorProducto(int productoId, int Cantidad)
        {
            ProductoLoteBuscarDto productoSeleccionado= new ProductoLoteBuscarDto();
            productoSeleccionado.ProductoId = productoId;
            productoSeleccionado.Cantidad= Cantidad;
            var resultado = _serviceLote.ObtenerLotesPorCantidad(productoSeleccionado);
            return Ok(resultado);

        }

        [HttpPost]
        [Route("Agregar")]
        public IActionResult AgregarProductoLote([FromBody] ProductoLoteIngresarDto nuevoLote)
        {
          
            var resultado = _serviceLote.IngresarNuevoLoteProductos(nuevoLote);
            return Ok(resultado);

        }

    }
}
