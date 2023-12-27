using Academia.SistemaGestionInventario.WApi._Common;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi.Domain.General
{
    public class GeneralDomain
    {

        public Respuesta<List<T>> ValidarListaNoEmpty<T>(List<T> elementos)
        {

            if (elementos == null || elementos.Count < 1)
            {
                return Respuesta<List<T>>.Fault(Mensajes.FAIL_DATA_NOT_EXIST, CodigoError.CODIGO400, elementos);

            }


            return Respuesta<List<T>>.Success();

        }
    }
}
