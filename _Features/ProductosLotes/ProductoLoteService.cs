using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Productos;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.ProductoLote;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;


namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes
{
    public class ProductoLoteService
    {
        GeneralDomain _generalDomain;
        ProductoLoteDomain _productoLoteDomain;
        private readonly IUnitOfWork _unitOfWork;


        public ProductoLoteService(UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain, ProductoLoteDomain productoLoteDomain)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _productoLoteDomain = productoLoteDomain;

        }

        public Respuesta<List<ProductoLoteDto>> ObtenerLotesPorCantidad(ProductoLoteBuscarDto productoseleccionado)
        {
            List<ProductoLoteDto> productosLoteDtos = new();

            ProductLoteDtoValidator validator = new();

            ValidationResult validationResult = validator.Validate(productoseleccionado);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(", ", errores);
                return Respuesta.Fault<List<ProductoLoteDto>>(menssageValidation, CodigoError.CODIGO400);
            }

            Producto productoDatos = _unitOfWork.Repository<Producto>().FirstOrDefault(u => u.ProductoId == productoseleccionado.ProductoId);

            if (productoDatos == null)
            {
                return Respuesta.Fault<List<ProductoLoteDto>>(Mensajes.PRODUCT_NOT_EXIST, CodigoError.CODIGO400);
            }

            int cantidadinventario = InventarioExistente(productoseleccionado.ProductoId);

            Respuesta<int> comprobarInventario = _productoLoteDomain.ValidarCantidadSalida(productoseleccionado.Cantidad, cantidadinventario);

            if (!comprobarInventario.Ok)
            {
                return Respuesta<List<ProductoLoteDto>>.Fault(comprobarInventario.Mensaje, comprobarInventario.Codigo, productosLoteDtos);

            }

            int cantidadRestante = productoseleccionado.Cantidad;

            List<ProductosLote> lotesDisponibles = _unitOfWork.Repository<ProductosLote>()
                   .Where(pl => pl.ProductoId == productoseleccionado.ProductoId && pl.Inventario > 0)
                   .OrderBy(pl => pl.FechaVencimiento)
                   .ToList();

            foreach (ProductosLote lote in lotesDisponibles)
            {
                int cantidadAUtilizar = Math.Min(cantidadRestante, lote.Inventario);
                if (cantidadAUtilizar > 0)
                {
                    productosLoteDtos.Add(new ProductoLoteDto
                    {
                        LoteId = lote.LoteId,
                        Costo = lote.CostoUnidad,
                        FechaVencimiento = lote.FechaVencimiento ,
                        Cantidad = cantidadAUtilizar,
                        ProductoId= productoseleccionado.ProductoId,
                        ProductoNombre= productoDatos.Nombre

                    });

                    cantidadRestante -= cantidadAUtilizar;
                }

                if (cantidadRestante <= 0)
                {
                    break;
                }
            }

            var ListEmpty = _generalDomain.ValidarListaNoEmpty(productosLoteDtos);

            if (!ListEmpty.Ok)
            {
                return Respuesta<List<ProductoLoteDto>>.Fault(ListEmpty.Mensaje, ListEmpty.Codigo, productosLoteDtos);

            }



            return Respuesta<List<ProductoLoteDto>>.Success(productosLoteDtos);


        }

        public int InventarioExistente(int idProduct)
        {
            int total = 0;
          
                total = _unitOfWork.Repository<ProductosLote>()
                .Where(pl => pl.ProductoId == idProduct && pl.Inventario > 0)
                .Sum(pl => pl.Inventario);


                return total;

        }
    }
}
