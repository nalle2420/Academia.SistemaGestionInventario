using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes
{
    public interface IProductoLoteService
    {
        Respuesta<List<ProductoLoteDto>> ObtenerLotesPorCantidad(ProductoLoteBuscarDto productoseleccionado);
        Respuesta<bool> DisminuirInventarioPorSalidaASucursal(int LoteId, int cantidad);

        Respuesta<ProductoLoteMostrarDto> IngresarNuevoLoteProductos(ProductoLoteIngresarDto productoLoteNuevo);
    }
}
