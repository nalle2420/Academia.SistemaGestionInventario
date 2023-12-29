using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
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

        public Respuesta<bool> ValidarFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            if (fechaInicio > fechaFinal)
            {
                return Respuesta<bool>.Fault(Mensajes.FAIL_DATE_INITIAL, CodigoError.CODIGO400,false);
            }

            if (fechaInicio > DateTime.Today)
            {
                return Respuesta<bool>.Fault(Mensajes.FAIL_DATE_INITIAL_TODAY, CodigoError.CODIGO400, false);
            }
            if (fechaInicio == DateTime.MinValue || fechaFinal == DateTime.MinValue)
            {
                return Respuesta<bool>.Fault(Mensajes.FAIL_DATE, CodigoError.CODIGO400, false);

            }
            return Respuesta<bool>.Success(true);


        }

        public Respuesta<Sucursal> validadSucursal(Sucursal? sucursal)
        {
            if (sucursal == null || sucursal.Activo==false)
            {
                return Respuesta<Sucursal>.Fault(Mensajes.SUCURSAL_NOT_EXIST, CodigoError.CODIGO400, sucursal);

            }

            return Respuesta<Sucursal>.Success(sucursal);
        
        }

        public Respuesta<SalidaInventario> validadSalida(SalidaInventario? salida)
        {
            if (salida == null || salida.Activo == false)
            {
                return Respuesta<SalidaInventario>.Fault(Mensajes.SALIDA_INVENTARIO_NOT_EXIST, CodigoError.CODIGO400, salida);

            }
            if (salida.EstadoId == ((int)ListEstados.Recibido))
            {
                return Respuesta<SalidaInventario>.Fault(Mensajes.NO_ESTADO, CodigoError.CODIGO400, salida);

            }

            return Respuesta<SalidaInventario>.Success(salida);

        }
    }
}
