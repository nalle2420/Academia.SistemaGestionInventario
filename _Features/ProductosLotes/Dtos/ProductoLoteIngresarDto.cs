using Academia.SistemaGestionInventario.WApi._Common;
using FluentValidation;

namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos
{
    public class ProductoLoteIngresarDto
    {
        public int ProductoId { get; set; }

        public int CantidadInicial { get; set; }

        public decimal CostoUnidad { get; set; }

        public DateTime FechaVencimiento { get; set; }
    }

    public class ProductLoteIngresarDtoValidator : AbstractValidator<ProductoLoteIngresarDto>
    {
        public ProductLoteIngresarDtoValidator()
        {
            RuleFor(v => v.ProductoId)
                .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.CantidadInicial)
               .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.CostoUnidad)
               .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.FechaVencimiento)
             .NotEmpty().WithMessage(Mensajes.FAIL_EMPTYORNULL);
        }


    }
}
