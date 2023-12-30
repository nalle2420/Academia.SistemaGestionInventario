namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos
{
    public class ProductoLoteMostrarDto
    {
        public int LoteId { get; set; }

        public int ProductoId { get; set; }

        public int CantidadInicial { get; set; }

        public decimal CostoUnidad { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int Inventario { get; set; }

        public bool Activo { get; set; }
    }
}
