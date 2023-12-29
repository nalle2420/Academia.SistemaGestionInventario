using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi.Domain.UsuarioD
{
    public class UsuarioDomain
    {
        public Respuesta<Usuario> ValidarLogin(Usuario usuario, byte[] claveIngresada)
        {

            if (HashedString(usuario.Clave) != HashedString(claveIngresada))
            {
                return Respuesta<Usuario>.Fault(Mensajes.LOGIN_FAIL, CodigoError.CODIGO400, usuario);

            }

            return Respuesta<Usuario>.Success(usuario);
        }

        public string HashedString(byte[] hashBytes)
        {
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}
