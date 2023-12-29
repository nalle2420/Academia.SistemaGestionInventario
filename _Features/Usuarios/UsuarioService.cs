using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Domain.General;
using Academia.SistemaGestionInventario.WApi.Domain.UsuarioD;
using Academia.SistemaGestionInventario.WApi.Infrastructure;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Academia.SistemaGestionInventario.WApi._Features.Usuarios
{
    public class UsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        GeneralDomain _generalDomain;
        UsuarioDomain _usuarioDomain;

        public UsuarioService(UnitOfWorkBuilder unitOfWorkBuilder, IMapper mapper, GeneralDomain generalDomain, UsuarioDomain usuarioDomain)
        {
            _unitOfWork = unitOfWorkBuilder.BuilderSistemaGestionInventario();
            _mapper = mapper;
            _generalDomain = generalDomain;
            _usuarioDomain = usuarioDomain;
        }

        private byte[] GenerateSHA256Hash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);

                return hashBytes;
            }
        }



     

        public Usuario? buscarUsuario(string usuarioNombre) => _unitOfWork.Repository<Usuario>().AsQueryable().FirstOrDefault(e => e.Nombre == usuarioNombre);


        public Respuesta<UsuarioDto> ObtenerUsuarioPorUsuarioYClave(UsuarioCredencialesDto credencialesUsuario)
        {

            Usuario? usuario;
            UsuarioDto usuarioDto = new UsuarioDto();
            try
            {
                usuario = buscarUsuario(credencialesUsuario.Nombre);

                Respuesta<Usuario> validarUsuarioExiste = _generalDomain.validarUsuario(usuario);
                if (!validarUsuarioExiste.Ok)
                {
                    return Respuesta<UsuarioDto>.Fault(Mensajes.LOGIN_FAIL, validarUsuarioExiste.Codigo, usuarioDto);

                }

                Respuesta<Usuario> validarLogin = _usuarioDomain.ValidarLogin(usuario,GenerateSHA256Hash(credencialesUsuario.Clave));
                if (!validarLogin.Ok)
                {
                    return Respuesta<UsuarioDto>.Fault(validarLogin.Mensaje, validarLogin.Codigo, usuarioDto);
                }
                usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                return Respuesta<UsuarioDto>.Success(usuarioDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Respuesta<UsuarioDto>.Fault(Mensajes.ERROR_AL_BUSCAR, CodigoError.CODIGO500, usuarioDto);
            }
        }
    }
}
