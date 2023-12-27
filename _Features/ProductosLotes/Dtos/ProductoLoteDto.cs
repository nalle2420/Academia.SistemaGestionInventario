namespace Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Dtos
{
    public class ProductoLoteDto
    {
        public int ProductoId { get; set; }
        
        public string ProductoNombre { get; set; }

        public int Cantidad { get; set; }

        public int LoteId { get; set; }

        public decimal Costo { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public ProductoLoteDto()
        {
            ProductoNombre = string.Empty;
        }

    }

}
