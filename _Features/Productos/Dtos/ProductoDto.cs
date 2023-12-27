namespace Academia.SistemaGestionInventario.WApi._Features.Productos.Dtos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public ProductoDto()
        {
            Nombre = string.Empty;
        }
    }
}
