using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;

namespace Academia.SistemaGestionInventario.WApi._Features.Sucursales
{
    public class SucursalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        GeneralDomain _generalDomain;


        public SucursalService( UnitOfWorkBuilder unitOfWorkBuilder, IMapper mapper, GeneralDomain generalDomain)
        {
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _mapper = mapper;
            _generalDomain = generalDomain;



        }

        public Respuesta<List<SucursalDto>> BuscarSucursales() {

            List<SucursalDto> sucursales = new List<SucursalDto>();
            try
            {
               sucursales= _unitOfWork.Repository<Sucursal>().AsQueryable()
                    .Where(sucursal => sucursal.Activo == true)
                    
                    .Select(sucursalDato => new SucursalDto
                    {
                        SucursalId = sucursalDato.SucursalId,
                        Nombre = sucursalDato.Nombre
                    }).ToList();

                var ListaVacia = _generalDomain.ValidarListaNoEmpty(sucursales);

                if (!ListaVacia.Ok)
                {
                    return Respuesta<List<SucursalDto>>.Fault(ListaVacia.Mensaje, ListaVacia.Mensaje, sucursales);

                }

                return Respuesta<List<SucursalDto>>.Success(sucursales);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Busqueda de sucursales" + ex);
                return Respuesta<List<SucursalDto>>.Fault(ex.ToString(), CodigoError.CODIGO500, sucursales);

            }


        }

    }
}
