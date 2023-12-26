using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities
{
    public class Sucursal
    {
        public int SucursalId { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }
        public int CreadoPor { get; set; }

        public DateTime CreadoEl { get; set; }

        public int ModificadoPor { get; set; }

        public DateTime ModificadoEl { get; set; }

        public virtual List<SalidaInventario> SalidaInventario { get; set; } = new List<SalidaInventario>();



        public Sucursal()
        {
            Nombre = string.Empty;
        }

    }
}
