using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario
{
    public interface ISalidaInventarioService
    {
        Respuesta<SalidaInventarioIngresarDto> InsertarSalidaInventario(SalidaInventarioIngresarDto nuevaSalidaAIngresar);
        Respuesta<SalidaInventarioDto> EditarEstado(SalidaEditEstadoDto editestado);

        Respuesta<List<SalidaListadoDto>> ListadoSalidasInventario(DateTime fechaInicio, DateTime fechaFinal, int sucursalID);
    }
}
