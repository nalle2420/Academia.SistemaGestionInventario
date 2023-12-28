using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Estados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario
{
    public class SalidaInventarioService
    {

        GeneralDomain _generalDomain;
        SalidaInventarioDomain _salidaInventarioDomain;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        SalidaInventarioDetalleService _serviceSalidaDetalle;



        public SalidaInventarioService(SalidaInventarioDetalleService salidaInventarioDetalleService,UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain, SalidaInventarioDomain salidaInventarioDomain, IMapper mapper)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _salidaInventarioDomain = salidaInventarioDomain;
            _mapper = mapper;
            _serviceSalidaDetalle = salidaInventarioDetalleService;

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

        public Respuesta<bool> ValidarExistenciaDatosSalida(SalidaInventarioIngresarDto salidaInventario)
        {
            SalidaInventarioValidator validator = new();

            ValidationResult validationResult = validator.Validate(salidaInventario);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(", ", errores);
                return Respuesta.Fault<bool>(menssageValidation, CodigoError.CODIGO400);
            }


            bool usuarioExistente = _unitOfWork.Repository<Usuario>().AsQueryable().Any(e => e.UsuarioId == salidaInventario.UsuarioId);
            if (!usuarioExistente)
            {
               return Respuesta<bool>.Fault(Mensajes.USER_NOT_EXIST+salidaInventario.UsuarioId, CodigoError.CODIGO400, false);
            }
            bool sucursalExistente = _unitOfWork.Repository<Sucursal>().AsQueryable().Any(e => e.SucursalId == salidaInventario.SucursalId);

            if (!sucursalExistente)
            {
               return Respuesta<bool>.Fault(Mensajes.SUCURSAL_NOT_EXIST, CodigoError.CODIGO400, false);
            }

            return Respuesta<bool>.Success(true);

        }

        public Respuesta<SalidaInventarioIngresarDto> InsertarSalidaInventario(SalidaInventarioIngresarDto nuevaSalidaAIngresar)
        {
            SalidaInventario salidaAAgregar = _mapper.Map<SalidaInventario>(nuevaSalidaAIngresar);
            Respuesta<bool> validacionDatosSalida = ValidarExistenciaDatosSalida(nuevaSalidaAIngresar);

            if (!validacionDatosSalida.Ok)
            {
                return Respuesta<SalidaInventarioIngresarDto>.Fault(validacionDatosSalida.Mensaje, validacionDatosSalida.Codigo, nuevaSalidaAIngresar);
            }

            decimal tarifaActual = ObtenerTarifaActual(nuevaSalidaAIngresar.SucursalId);


            decimal Total = nuevaSalidaAIngresar.listaDetalleLotes.Sum(item => item.Cantidad*item.Costo);


            Respuesta<decimal> validarTarifaFinal= _salidaInventarioDomain.VerificarTarifa(Total, tarifaActual);

            if (!validarTarifaFinal.Ok)
            {
                return Respuesta<SalidaInventarioIngresarDto>.Fault(validarTarifaFinal.Mensaje, validarTarifaFinal.Codigo, nuevaSalidaAIngresar);

            }
            salidaAAgregar.Activo = true;
            salidaAAgregar.EstadoId = ((int)ListEstados.EnviadoASucursal);
            salidaAAgregar.CreadoEl= DateTime.Now;
            salidaAAgregar.CreadoPor = 1;
            salidaAAgregar.Total = Total;

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.Repository<SalidaInventario>().Add(salidaAAgregar);
                _unitOfWork.SaveChanges();

                foreach(var detalleSalidaPorLote in nuevaSalidaAIngresar.listaDetalleLotes)
                {
                    SalidaInventarioDetalle detalleSalidaInventario= _mapper.Map<SalidaInventarioDetalle>(detalleSalidaPorLote);
                    detalleSalidaInventario.Activo= true;
                    detalleSalidaInventario.SalidaInventarioId = salidaAAgregar.SalidaInventarioId;
                   Respuesta<SalidaInventarioDetalle> respuestaInsertarDetalle =_serviceSalidaDetalle.InsertarDetalleSalida(detalleSalidaInventario);
                    if (!respuestaInsertarDetalle.Ok)
                    {
                        _unitOfWork.RollBack();
                        return Respuesta<SalidaInventarioIngresarDto>.Fault(respuestaInsertarDetalle.Mensaje, respuestaInsertarDetalle.Codigo, nuevaSalidaAIngresar);

                    }
                }
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();
                return Respuesta<SalidaInventarioIngresarDto>.Success(nuevaSalidaAIngresar);


            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Respuesta<SalidaInventarioIngresarDto>.Fault(ex.ToString(), CodigoError.CODIGO500, nuevaSalidaAIngresar);

            }


        } 

        public Respuesta<List<SalidaListadoDto>> ListadoSalidasInventario(DateTime fechaInicio, DateTime fechaFinal, int sucursalID)
        {
            List<SalidaListadoDto> listaSalida = new List<SalidaListadoDto>();
            try
            {
                listaSalida = (from Salida in _unitOfWork.Repository<SalidaInventario>().AsQueryable()
                              join Detalle in _unitOfWork.Repository<SalidaInventarioDetalle>().AsQueryable() on Salida.SalidaInventarioId equals Detalle.SalidaInventarioId into detallesGrupo
                              join estado in _unitOfWork.Repository<Estado>().AsQueryable() on Salida.EstadoId equals estado.EstadoId
                            join usuario in _unitOfWork.Repository<Usuario>().AsQueryable() on Salida.UsuarioIdRecibe equals usuario.UsuarioId
                            join empleado in _unitOfWork.Repository<Empleado>().AsQueryable() on usuario.EmpleadoId equals empleado.EmpleadoId
                              join sucursal in _unitOfWork.Repository<Sucursal>().AsQueryable() on Salida.SucursalId equals sucursal.SucursalId
                               where Salida.FechaSalida >= fechaInicio
                               && Salida.FechaSalida <= fechaFinal
                               && Salida.SucursalId == sucursalID
                               select new SalidaListadoDto
                               {
                                   SalidaInventarioId = Salida.SalidaInventarioId,
                                  SucursalId = sucursal.SucursalId,
                                   SucursalNombre = sucursal.Nombre,
                                   Fecha = Salida.FechaSalida,
                                  Cantidad = detallesGrupo.Sum(d => d.CantidadProducto),
                                   Total = Salida.Total,
                                  FechaRecibido = Salida.FechaRecibido,
                                   Empleadorecibe= empleado.Nombre,
                                  Estado = estado.EstadoNombre
                               }
                              ).ToList();
                

                return Respuesta<List<SalidaListadoDto>>.Success(listaSalida);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Lista salida" + ex);
                return Respuesta<List<SalidaListadoDto>>.Fault(Mensajes.ERROR_AL_BUSCAR, CodigoError.CODIGO500, listaSalida);
            }
        }
    }
}
