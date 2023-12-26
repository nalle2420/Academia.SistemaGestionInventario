namespace Academia.SistemaGestionInventario.WApi._Features.Sucursales.Dtos
{
    public class SucursalDto
    {
        public int SucursalId { get; set; }

        public string Nombre { get; set; }


        public SucursalDto()
        {
            Nombre = string.Empty;
        }
    }
}
