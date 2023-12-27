using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario
{
    public class SalidaInventarioService
    {

        GestionInventarioDbContext _context;
        GeneralDomain _generalDomain;
        SalidaInventarioDomain _salidaInventarioDomain;
        private readonly IUnitOfWork _unitOfWork;


        public SalidaInventarioService(UnitOfWorkBuilder unitOfWorkBuilder, GestionInventarioDbContext context, GeneralDomain generalDomain, SalidaInventarioDomain salidaInventarioDomain)
        {
            _context = context;
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _salidaInventarioDomain = salidaInventarioDomain;

        }

        private decimal ObtenerTarifaActual(int sucursal)
        {
            try
            {
                decimal sumaTarifa = _unitOfWork.Repository<SalidaInventario>()
              .Where(salida => salida.EstadoId == (int)ListEstados.EnviadoASucursal && salida.SucursalId == sucursal)
              .Sum(salida => salida.Total);

                return sumaTarifa;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public Respuesta<SalidaInventarioIngresarDto> InsertarSalidaInventario(SalidaInventarioIngresarDto nuevaSalidaAIngresar)
        {
            decimal tarifaActual = ObtenerTarifaActual(nuevaSalidaAIngresar.SucursalId);

            Respuesta<decimal> validarTarifaFinal= _salidaInventarioDomain.VerificarTarifa(nuevaSalidaAIngresar.Total, tarifaActual);

            if (!validarTarifaFinal.Ok)
            {
                return Respuesta<SalidaInventarioIngresarDto>.Fault(validarTarifaFinal.Mensaje, validarTarifaFinal.Codigo, nuevaSalidaAIngresar);

            }
            return Respuesta<SalidaInventarioIngresarDto>.Success(nuevaSalidaAIngresar);


        }
    }
}
