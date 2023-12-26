using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities
{
    public class ProductosLote
    {
        public int LoteId { get; set; }

        public int ProductoId { get; set; }

        public int CantidadInicial { get; set; }

        public decimal CostoUnidad { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int Inventario { get; set; }

        public bool Activo { get; set; }

        public int CreadoPor { get; set; }

        public DateTime CreadoEl { get; set; }

        public int ModificadoPor { get; set; }

        public DateTime ModificadoEl { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual List<SalidaInventarioDetalle> SalidaInventarioDetalle { get; set; } = new List<SalidaInventarioDetalle>();

    }
}
