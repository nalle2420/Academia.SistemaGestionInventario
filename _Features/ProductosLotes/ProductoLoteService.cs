using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Productos;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.ProductoLote;
using Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;


namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes
{
    public class ProductoLoteService: IProductoLoteService
    {
        GeneralDomain _generalDomain;
        ProductoLoteDomain _productoLoteDomain;
        private readonly IUnitOfWork _unitOfWork;
        SalidaInventarioDomain _salidaInventarioDomain;
        private readonly IMapper _mapper;



        public ProductoLoteService(IMapper mapper,UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain, ProductoLoteDomain productoLoteDomain, SalidaInventarioDomain salidaInventarioDomain)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _productoLoteDomain = productoLoteDomain;
            _salidaInventarioDomain = salidaInventarioDomain;
            _mapper = mapper;


        }

        public Respuesta<bool> DisminuirInventarioPorSalidaASucursal(int LoteId,int cantidad)
        {

           ProductosLote loteSeleccionado = _unitOfWork.Repository<ProductosLote>().FirstOrDefault(lote => lote.LoteId == LoteId)?? new ProductosLote();

           Respuesta<decimal> validacionTarifa= _salidaInventarioDomain.VerificarTarifa(cantidad, loteSeleccionado.Inventario);
            if (!validacionTarifa.Ok)
            {
                return Respuesta<bool>.Fault(validacionTarifa.Mensaje, validacionTarifa.Codigo, false);
            }

            loteSeleccionado.Inventario -= cantidad;

            return Respuesta<bool>.Success(true);



        }

        private Respuesta<bool> ValidarDatos(ProductoLoteIngresarDto productoLoteNuevo)
        {
            Respuesta<DateTime> comprobarFechaVencimiento = _productoLoteDomain.ValidarFechaVencimiento(productoLoteNuevo.FechaVencimiento);

            if (!comprobarFechaVencimiento.Ok)
            {
                return Respuesta<bool>.Fault(comprobarFechaVencimiento.Mensaje, comprobarFechaVencimiento.Codigo, false);

            }
            Producto productoDatos = ProductoExiste(productoLoteNuevo.ProductoId);

            Respuesta<Producto> comprobarProducto = _productoLoteDomain.validarproducto(productoDatos);


            if (!comprobarProducto.Ok)
            {
                return Respuesta<bool>.Fault(comprobarProducto.Mensaje, comprobarProducto.Codigo, false);

            }
            return Respuesta<bool>.Success(true);


        }
        public Respuesta<ProductoLoteMostrarDto> IngresarNuevoLoteProductos(ProductoLoteIngresarDto productoLoteNuevo)
        {
            ProductosLote nuevoLote = new();
            ProductoLoteMostrarDto productoLoteMostrarDto = new();

            try
            {
                ProductLoteIngresarDtoValidator validator = new();

                ValidationResult validationResult = validator.Validate(productoLoteNuevo);

                if (!validationResult.IsValid)
                {
                    IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                    string menssageValidation = string.Join(", ", errores);
                    return Respuesta.Fault<ProductoLoteMostrarDto>(menssageValidation, CodigoError.CODIGO400);
                }

                Respuesta<bool> validacionDatos = ValidarDatos(productoLoteNuevo);
                if (!validacionDatos.Ok)
                {
                    return Respuesta.Fault<ProductoLoteMostrarDto>(validacionDatos.Mensaje, CodigoError.CODIGO400);

                }

                nuevoLote = _mapper.Map<ProductosLote>(productoLoteNuevo);
                nuevoLote.Activo = true;
                nuevoLote.Inventario = nuevoLote.CantidadInicial;
                nuevoLote.CreadoPor = 3;
                nuevoLote.CreadoEl = DateTime.Now;

                _unitOfWork.Repository<ProductosLote>().Add(nuevoLote);
                _unitOfWork.SaveChanges();

                productoLoteMostrarDto = _mapper.Map<ProductoLoteMostrarDto>(nuevoLote);


                return Respuesta<ProductoLoteMostrarDto>.Success(productoLoteMostrarDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Agregar Lote" + ex);
                return Respuesta<ProductoLoteMostrarDto>.Fault(Mensajes.ERROR_AL_BUSCAR, CodigoError.CODIGO500, productoLoteMostrarDto);
            }

        }

        private Producto ProductoExiste(int idProducto)=> _unitOfWork.Repository<Producto>().FirstOrDefault(u => u.ProductoId == idProducto);

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

            Producto productoDatos = ProductoExiste(productoseleccionado.ProductoId);

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
