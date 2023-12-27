using Academia.SistemaGestionInventario.WApi._Common;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi.Domain.ProductoLote
{
    public class ProductoLoteDomain
    {
        public Respuesta<int> ValidarCantidadSalida(int ingresado, int inventario)
        {
            if (ingresado > inventario)
            {
                return Respuesta<int>.Fault(Mensajes.PRODUCTO_INSUFICIENTE, "400", inventario);
            }

            return Respuesta<int>.Success(inventario);
        }
    }
}
