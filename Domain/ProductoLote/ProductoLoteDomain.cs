using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
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

        public Respuesta<DateTime> ValidarFechaVencimiento(DateTime fechaVencimiento)
        {
            if (fechaVencimiento <= DateTime.Now)
            {
                return Respuesta<DateTime>.Fault(Mensajes.FAIL_FECHAVENCIMIENTO, "400", fechaVencimiento);
            }

            return Respuesta<DateTime>.Success(fechaVencimiento);
        }

        public Respuesta<Producto> validarproducto(Producto? producto)
        {
            if (producto == null || producto.Activo == false)
            {
                return Respuesta<Producto>.Fault(Mensajes.PRODUCT_NOT_EXIST, CodigoError.CODIGO400, producto);

            }

            return Respuesta<Producto>.Success(producto);

        }
    }
}
