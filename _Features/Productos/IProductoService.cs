using Academia.SistemaGestionInventario.WApi._Features.Productos.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.Productos
{
    public interface IProductoService
    {
        Respuesta<List<ProductoDto>> BuscarProductos();
    }
}
