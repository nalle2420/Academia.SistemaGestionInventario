using Academia.SistemaGestionInventario.WApi._Features.Sucursales;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.SistemaGestionInventario.WApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _serviceUsuario;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _serviceUsuario= usuarioService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult ValidarUsuarioPorUsuarioYClave(UsuarioCredencialesDto credencialesUsuario)
        {

            var resultado = _serviceUsuario.ObtenerUsuarioPorUsuarioYClave(credencialesUsuario);
            return Ok(resultado);

        }

       

    }
}
