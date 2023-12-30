using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.Sucursales
{
    public interface ISucursalService
    {
        Respuesta<List<SucursalDto>> BuscarSucursales();


     }
}
