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

        private Respuesta<bool> ValidarExistenciaDatosSalida(SalidaInventarioIngresarDto salidaInventario)
        {
            SalidaInventarioValidator validator = new();

            ValidationResult validationResult = validator.Validate(salidaInventario);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(", ", errores);
                return Respuesta.Fault<bool>(menssageValidation, CodigoError.CODIGO400);
            }

            Respuesta<Usuario> validarUsuarioExiste = _generalDomain.validarUsuario(buscarUsuario(salidaInventario.UsuarioId));
            if (!validarUsuarioExiste.Ok)
            {
                return Respuesta<bool>.Fault(validarUsuarioExiste.Mensaje, validarUsuarioExiste.Codigo, false);

            }

            Respuesta<Sucursal> validarSucursalExiste = _salidaInventarioDomain.validadSucursal(buscarSucursal(salidaInventario.SucursalId));
            if (!validarSucursalExiste.Ok)
            {
                return Respuesta<bool>.Fault(validarSucursalExiste.Mensaje, validarSucursalExiste.Codigo, false);

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
        public Respuesta<SalidaInventarioDto> EditarEstado(SalidaEditEstadoDto editestado)
        {
            SalidaInventario? salidaeditada = buscarSalida(editestado.SalidaInventarioId);
            SalidaInventarioDto salidaeditadaDto = new SalidaInventarioDto();

            try
            {
                Respuesta<SalidaInventario> validarSalidaExiste = _salidaInventarioDomain.validadSalida(salidaeditada);
                if (!validarSalidaExiste.Ok)
                {
                    return Respuesta<SalidaInventarioDto>.Fault(validarSalidaExiste.Mensaje, validarSalidaExiste.Codigo, salidaeditadaDto);

                }
                Respuesta<Usuario> validarUsuarioExiste = _generalDomain.validarUsuario(buscarUsuario(editestado.UsuarioIdrecibe));
                if (!validarUsuarioExiste.Ok)
                {
                    return Respuesta<SalidaInventarioDto>.Fault(validarUsuarioExiste.Mensaje, validarUsuarioExiste.Codigo, salidaeditadaDto);

                }
                salidaeditada.EstadoId = ((int)ListEstados.Recibido);
                salidaeditada.FechaRecibido = DateTime.Now;
                salidaeditada.UsuarioIdRecibe = editestado.UsuarioIdrecibe;
                salidaeditada.ModificadoPor= editestado.UsuarioIdrecibe;
                salidaeditada.ModificadoEl= DateTime.Now;

                _unitOfWork.SaveChanges();
                salidaeditadaDto = _mapper.Map<SalidaInventarioDto>(salidaeditada);

                return Respuesta<SalidaInventarioDto>.Success(salidaeditadaDto);


            }
            catch(Exception ex)
            {
                Console.WriteLine("Editar salida" + ex);
                return Respuesta<SalidaInventarioDto>.Fault(Mensajes.ERROR_AL_BUSCAR, CodigoError.CODIGO500, salidaeditadaDto);
            }
        }



        public Respuesta<List<SalidaListadoDto>> ListadoSalidasInventario(DateTime fechaInicio, DateTime fechaFinal, int sucursalID)
        {
            List<SalidaListadoDto> listaSalida = new List<SalidaListadoDto>();
            Respuesta<bool> validarFechas = _salidaInventarioDomain.ValidarFechas(fechaInicio,fechaFinal);

            if (!validarFechas.Ok)
            {
                return Respuesta<List<SalidaListadoDto>>.Fault(validarFechas.Mensaje, validarFechas.Codigo, listaSalida);

            }
            Respuesta<Sucursal> validarSucursalExiste = _salidaInventarioDomain.validadSucursal(buscarSucursal(sucursalID));
            if (!validarSucursalExiste.Ok)
            {
                return Respuesta<List<SalidaListadoDto>>.Fault(validarSucursalExiste.Mensaje, validarSucursalExiste.Codigo, listaSalida);

            }

            try
            {
                listaSalida = (from Salida in _unitOfWork.Repository<SalidaInventario>().AsQueryable()
                              join Detalle in _unitOfWork.Repository<SalidaInventarioDetalle>().AsQueryable() on Salida.SalidaInventarioId equals Detalle.SalidaInventarioId into detallesGrupo
                              join estado in _unitOfWork.Repository<Estado>().AsQueryable() on Salida.EstadoId equals estado.EstadoId
                            join usuario in _unitOfWork.Repository<Usuario>().AsQueryable() on Salida.UsuarioIdRecibe equals usuario.UsuarioId into usuarios
                            from usuario in usuarios.DefaultIfEmpty()
                            join empleado in _unitOfWork.Repository<Empleado>().AsQueryable() on usuario.EmpleadoId equals empleado.EmpleadoId into empleados
                            from empleado in empleados.DefaultIfEmpty()
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
                                   Empleadorecibe= empleado != null ? (empleado.Nombre+" "+ empleado.Apellido) : null,
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

        public Sucursal? buscarSucursal(int idSucursal) => _unitOfWork.Repository<Sucursal>().AsQueryable().FirstOrDefault(e => e.SucursalId == idSucursal);
        public Usuario? buscarUsuario(int idUsuario) => _unitOfWork.Repository<Usuario>().AsQueryable().FirstOrDefault(e => e.UsuarioId == idUsuario);

        public SalidaInventario? buscarSalida(int idSalida) => _unitOfWork.Repository<SalidaInventario>().AsQueryable().FirstOrDefault(e => e.SalidaInventarioId == idSalida);

    }
}
