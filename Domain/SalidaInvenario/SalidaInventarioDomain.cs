using Academia.SistemaGestionInventario.WApi._Common;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi.Domain.SalidaInvenario
{
    public class SalidaInventarioDomain
    {
        public Respuesta<decimal> VerificarTarifa(decimal totalAIngresar,decimal tarifaActual)

        {
            decimal tarifaFinal = totalAIngresar + tarifaActual;
            if (tarifaFinal > 5000)
            {
                return Respuesta<decimal>.Fault(Mensajes.NO_INGRESOS, CodigoError.CODIGO400, tarifaFinal);
            }

            return Respuesta<decimal>.Success(tarifaFinal);
        }
    }
}
