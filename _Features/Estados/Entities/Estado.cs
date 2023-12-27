using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Estados.Entities
{
    public class Estado
    {
        public int EstadoId { get; set; }

        public string EstadoNombre { get; set; }
        public bool Activo { get; set; }
        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }


        public virtual Producto Producto { get; set; }

        public virtual List<SalidaInventario> SalidasInventarios { get; set; } = new List<SalidaInventario>();


        public Estado()
        {
            EstadoNombre = string.Empty;
        }
    }
}
