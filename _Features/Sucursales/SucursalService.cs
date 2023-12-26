using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
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
        GestionInventarioDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SucursalService(GestionInventarioDbContext context, UnitOfWorkBuilder unitOfWorkBuilder, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _mapper = mapper;


        }

        public Respuesta<List<SucursalDto>> BuscarSucursales() {

            List<SucursalDto> sucursales = new List<SucursalDto>();
            try
            {
               sucursales= _unitOfWork.Repository<Sucursal>().AsQueryable()
                    .Where(sucursal => sucursal.Activo == true)
                    
                    .Select(sucursal => new SucursalDto
                    {
                        SucursalId = sucursal.SucursalId,
                        Nombre = sucursal.Nombre
                    }).ToList();

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
