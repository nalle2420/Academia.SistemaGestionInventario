using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using AutoMapper;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<SalidaInventario, SalidaInventarioIngresarDto>();
            CreateMap<SalidaInventarioIngresarDto, SalidaInventario>();
            CreateMap<SalidaInventarioIngresarDto, SalidaInventario>().ForMember(d => d.UsuarioId, o => o.MapFrom(s => s.UsuarioId))
                .ForMember(d => d.SucursalId, o => o.MapFrom(s => s.SucursalId))
                .ForMember(d => d.FechaSalida, o => o.MapFrom(s => s.Fecha));

            CreateMap<ProductoLoteDto,SalidaInventarioDetalle>().ForMember(d => d.LoteId, o => o.MapFrom(s => s.LoteId))
                .ForMember(d => d.CantidadProducto, o => o.MapFrom(s => s.Cantidad));

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<SalidaInventario, SalidaInventarioDto>();
            CreateMap<SalidaInventarioDto, SalidaInventario>();
            CreateMap<ProductoLoteIngresarDto, ProductosLote>();
            CreateMap<ProductosLote,ProductoLoteIngresarDto>();


            CreateMap<ProductoLoteMostrarDto, ProductosLote>();
            CreateMap<ProductosLote, ProductoLoteMostrarDto>();






        }

    }
}
