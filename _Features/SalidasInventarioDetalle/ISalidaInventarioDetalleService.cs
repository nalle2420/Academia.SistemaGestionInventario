using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle
{
    public interface ISalidaInventarioDetalleService
    {
        Respuesta<SalidaInventarioDetalle> InsertarDetalleSalida(SalidaInventarioDetalle nuevoDetalle);
    }
}
