namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario
{
    public class SalidaListadoDto
    {
        public int SalidaInventarioId { get; set; }

        public int? SucursalId { get; set; }

        public string? SucursalNombre { get; set; }

        public DateTime? Fecha { get; set; }

        public int? Cantidad { get; set; }

        public decimal Total { get; set; }

        public string? Empleadorecibe { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public string? Estado { get; set; }


        public SalidaListadoDto()
        {
            SucursalNombre= string.Empty;
            Empleadorecibe= string.Empty;
            Estado= string.Empty;
        }


    }
}
