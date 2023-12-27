using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Productos.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }

        public virtual List<ProductosLote> ProductosLotes { get; set; } = new List<ProductosLote>();


        public Producto()
        {
            Nombre = string.Empty;
        }

    }
}
