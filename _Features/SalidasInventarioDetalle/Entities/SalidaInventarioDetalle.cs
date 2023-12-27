using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities
{
    public class SalidaInventarioDetalle
    {
        public int SalidaDetalleId { get; set; }

        public int SalidaInventarioId { get; set; }

        public int LoteId { get; set; }

        public int CantidadProducto { get; set; }

        public bool Activo { get; set; }
        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }

        public virtual ProductosLote Lote { get; set; }

        public virtual SalidaInventario SalidaInventario { get; set; }
    }
}
