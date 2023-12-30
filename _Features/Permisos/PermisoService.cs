using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities;
using Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Permisos.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Permisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi._Features.Permisos
{
    public class PermisoService: IPermisoServices
    {
        GeneralDomain _generalDomain;
        private readonly IUnitOfWork _unitOfWork;

        public PermisoService(UnitOfWorkBuilder unitOfWorkBuilder, GeneralDomain generalDomain)
        {
            _generalDomain = generalDomain;
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
        }

        public Usuario? buscarUsuario(int idUsuario) => _unitOfWork.Repository<Usuario>().AsQueryable().FirstOrDefault(e => e.UsuarioId == idUsuario);

        public Respuesta<List<PermisoDto>> ObtenerPermisosPorUsuario(int usuarioId)
        {
            List<PermisoDto> permisosPorUsuario = new List<PermisoDto>();

            Respuesta<Usuario> validarUsuarioExiste = _generalDomain.validarUsuario(buscarUsuario(usuarioId));
            if (!validarUsuarioExiste.Ok)
            {
                return Respuesta<List<PermisoDto>>.Fault(validarUsuarioExiste.Mensaje, validarUsuarioExiste.Codigo, permisosPorUsuario);
            }

            permisosPorUsuario = (
                from usuario in _unitOfWork.Repository<Usuario>().AsQueryable()
                join perfil in _unitOfWork.Repository<Perfil>().AsQueryable() on usuario.PerfilId equals perfil.PerfilId
                join perfilPorPermiso in _unitOfWork.Repository<PerfilPorPermiso>().AsQueryable() on perfil.PerfilId equals perfilPorPermiso.PerfilId
                join permiso in _unitOfWork.Repository<Permiso>().AsQueryable() on perfilPorPermiso.PermisoId equals permiso.PermisoId
                where usuario.UsuarioId == usuarioId 
                select new PermisoDto
                {
                    PermisoId = permiso.PermisoId,
                    PermisoNombre = permiso.PermisoNombre
                }
            ).ToList();


            var ListEmpty = _generalDomain.ValidarListaNoEmpty(permisosPorUsuario);
            if (!ListEmpty.Ok)
            {
                return Respuesta<List<PermisoDto>>.Fault(Mensajes.FAIL_NOT_PERMISSIONS, ListEmpty.Codigo, ListEmpty.Data);

            }

            return Respuesta<List<PermisoDto>>.Success(permisosPorUsuario);

        }
    }
}
