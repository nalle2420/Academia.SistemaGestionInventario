namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos
{
    public class SalidaInventarioDto
    {
        public int SalidaInventarioId { get; set; }

        public int SucursalId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaSalida { get; set; }

        public decimal Total { get; set; }

        public DateTime? FechaRecibido { get; set; }

        public int? UsuarioIdRecibe { get; set; }

        public int EstadoId { get; set; }

        public bool Activo { get; set; }

    }
}
