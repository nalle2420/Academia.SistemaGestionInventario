using Academia.SistemaGestionInventario.WApi._Features.Sucursales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly SucursalService _serviceSucursal;
        public SucursalController(SucursalService sucursalService)
        {
            _serviceSucursal = sucursalService;

        }


        [HttpGet]
        [Route("sucursales")]
        public IActionResult ObtenerSucursales()
        {

            var resultado = _serviceSucursal.BuscarSucursales();
            return Ok(resultado);

        }
    }
}
