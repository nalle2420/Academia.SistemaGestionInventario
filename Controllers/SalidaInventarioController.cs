using Academia.SistemaGestionInventario.WApi._Features.Productos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaInventarioController : ControllerBase
    {
        private readonly SalidaInventarioService _serviceSalidaInventario;

        public SalidaInventarioController(SalidaInventarioService serviceSalidaInventario)
        {
            _serviceSalidaInventario = serviceSalidaInventario;
        }

        [HttpPost]
        [Route("Registrar")]
        public IActionResult RegistrarSalidaInventario([FromBody] SalidaInventarioIngresarDto nuevaSalida)
        {

            var resultado = _serviceSalidaInventario.InsertarSalidaInventario(nuevaSalida);
            return Ok(resultado);

        }

        [HttpPut]
        [Route("ActualizarEstado")]
        public IActionResult ActualizarEstadoSalida([FromBody] SalidaEditEstadoDto salidaAEditar)
        {

            var resultado = _serviceSalidaInventario.EditarEstado(salidaAEditar);
            return Ok(resultado);

        }

        [HttpGet]
        [Route("listado")]

         public IActionResult ListadoSalidaInventarioPorSucursal(DateTime fechaInicio, DateTime fechaFinal, int sucursalID)
        {

            var resultado = _serviceSalidaInventario.ListadoSalidasInventario(fechaInicio,fechaFinal,sucursalID);
            return Ok(resultado);

        }
    }
}
