using Academia.SistemaGestionInventario.WApi._Common;
using FluentValidation;

namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos
{
    public class ProductoLoteBuscarDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

    }
    public class ProductLoteDtoValidator : AbstractValidator<ProductoLoteBuscarDto>
    {
        public ProductLoteDtoValidator()
        {
            RuleFor(v => v.ProductoId)
                .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
            RuleFor(v => v.Cantidad)
               .GreaterThan(0).WithMessage(Mensajes.FAIL_NONVALUE_ZERO);
        }

       
    }
}
