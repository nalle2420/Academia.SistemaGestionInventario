using Academia.SistemaGestionInventario.WApi._Features.Permisos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly PermisoService _servicePermiso;

        public PermisoController(PermisoService permisoService)
        {
            _servicePermiso = permisoService;
        }

        [HttpGet]
        [Route("permiso")]
        public IActionResult ObtenerPermisosPorUsuario(int idUsuario)
        {

            var resultado = _servicePermiso.ObtenerPermisosPorUsuario(idUsuario);
            return Ok(resultado);

        }
    }
}
