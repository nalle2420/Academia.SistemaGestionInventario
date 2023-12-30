using Academia.SistemaGestionInventario.WApi._Features.Permisos.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.Permisos
{
    public interface IPermisoServices
    {
        Respuesta<List<PermisoDto>> ObtenerPermisosPorUsuario(int usuarioId);
    }
}
