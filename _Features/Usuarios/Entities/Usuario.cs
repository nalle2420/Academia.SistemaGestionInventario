using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public Byte[] Clave { get; set; }

        public int EmpleadoId { get; set; }

        public int PerfilId { get; set; }

        public bool Activo {  get; set; }

        public int? CreadoPor {  get; set; }

        public DateTime? CreadoEl {  get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }

        public virtual Empleado Empleado { get; set; } 

        public virtual Perfil? Perfil { get; set; }

        public virtual List<SalidaInventario> SalidaInventario { get; set; } = new List<SalidaInventario>();


        public Usuario()
        {
            Nombre = string.Empty;

        }
    }
}
