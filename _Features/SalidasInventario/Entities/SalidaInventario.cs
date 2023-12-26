using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Estados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities
{
    public class SalidaInventario
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
        public int CreadoPor { get; set; }

        public DateTime CreadoEl { get; set; }

        public int ModificadoPor { get; set; }

        public DateTime ModificadoEl { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Sucursal Sucursal { get; set; }

        public virtual List<SalidaInventarioDetalle> SalidaInventarioDetalle { get; set; } = new List<SalidaInventarioDetalle>();

    }
}
