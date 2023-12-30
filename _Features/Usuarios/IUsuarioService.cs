using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.Usuarios
{
    public interface IUsuarioService
    {
        Respuesta<UsuarioDto> ObtenerUsuarioPorUsuarioYClave(UsuarioCredencialesDto credencialesUsuario);
    }
}
