using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos;
using FluentValidation;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos
{
    public class SalidaInventarioIngresarDto
    {
        public int SucursalId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal Total { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public int? UsuarioIdrecibe { get; set; }

    }
    public class SalidaInventarioValidator : AbstractValidator<SalidaInventarioIngresarDto>
    {
        public SalidaInventarioValidator()
        {
            RuleFor(v => v.SucursalId)
                .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.UsuarioId)
               .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.Total)
              .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
        }


    }
}
