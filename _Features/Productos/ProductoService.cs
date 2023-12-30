using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.SistemaGestionInventario.WApi._Features.Productos
{
    public class ProductoService: IProductoService
    {
        GeneralDomain _generalDomain;
        private readonly IUnitOfWork _unitOfWork;


        public ProductoService(UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
        }

        public Respuesta<List<ProductoDto>> BuscarProductos()

        {
            List<ProductoDto> productos = new List<ProductoDto>();
            try
            {
                productos = _unitOfWork.Repository<Producto>()
                    .Where(producto => producto.Activo ==true)
                    .Select(s => new ProductoDto
                    {
                        ProductoId = s.ProductoId,
                        Nombre = s.Nombre
                    })
                    .ToList();

                var ListEmpty = _generalDomain.ValidarListaNoEmpty(productos);

                if (!ListEmpty.Ok)
                {
                    return Respuesta<List<ProductoDto>>.Fault(ListEmpty.Mensaje, ListEmpty.Codigo, ListEmpty.Data);

                }

                return Respuesta<List<ProductoDto>>.Success(productos);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Busqueda de productos" + ex);
                return Respuesta<List<ProductoDto>>.Fault(Mensajes.ERROR_AL_BUSCAR, CodigoError.CODIGO500, productos);
            }
        }
    }
}
