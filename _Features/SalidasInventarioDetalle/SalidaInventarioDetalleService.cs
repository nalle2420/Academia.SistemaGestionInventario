using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle
{
    public class SalidaInventarioDetalleService: ISalidaInventarioDetalleService
    {
        GeneralDomain _generalDomain;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IProductoLoteService _productoLoteService;

        public SalidaInventarioDetalleService(UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain, IMapper mapper, IProductoLoteService productoLoteService)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _mapper = mapper;
            _productoLoteService= productoLoteService;
        }

        public Respuesta<SalidaInventarioDetalle> InsertarDetalleSalida(SalidaInventarioDetalle nuevoDetalle)
        {
            try
            {   bool productoLoteExiste = _unitOfWork.Repository<ProductosLote>().AsQueryable().Any(lote=> lote.LoteId== nuevoDetalle.LoteId);
                if (!productoLoteExiste)
                {
                    Respuesta<SalidaInventarioDetalle>.Fault(Mensajes.LOTE_NOT_EXIST, CodigoError.CODIGO400);
                }

                _unitOfWork.Repository<SalidaInventarioDetalle>().Add(nuevoDetalle);
               Respuesta<bool> disminuirinventario= _productoLoteService.DisminuirInventarioPorSalidaASucursal(nuevoDetalle.LoteId, nuevoDetalle.CantidadProducto);
                if( !disminuirinventario.Ok)
                {
                    Respuesta<SalidaInventarioDetalle>.Fault(disminuirinventario.Mensaje+"deta", disminuirinventario.Codigo);
                }
                return Respuesta<SalidaInventarioDetalle>.Success(nuevoDetalle);
            }
            catch (Exception ex)
            {
                return Respuesta.Fault<SalidaInventarioDetalle>(ex.ToString(),CodigoError.CODIGO500);

            }
        }
    }
}
