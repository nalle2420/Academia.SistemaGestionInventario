using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using FluentValidation;

namespace Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos
{
    public class UsuarioCredencialesDto
    {
        public string Nombre { get; set; }
        public string Clave { get; set; }

        public UsuarioCredencialesDto()
        {
            Nombre = string.Empty;
            Clave = string.Empty;
        }
    }
    public class UsuarioValidator : AbstractValidator<UsuarioCredencialesDto>
    {
        public UsuarioValidator()
        {
            RuleFor(v => v.Nombre)
                .NotEmpty().WithMessage(Mensajes.FAIL_EMPTYORNULL);
            RuleFor(v => v.Clave)
               .NotEmpty().WithMessage(Mensajes.FAIL_EMPTYORNULL);
        }


    }

}
