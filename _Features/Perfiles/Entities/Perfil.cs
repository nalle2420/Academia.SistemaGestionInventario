using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Permisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities
{
    public class Perfil
    {
        public int PerfilId { get; set; }

        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }

        public virtual List<PerfilPorPermiso> PerfilPorPermiso { get; set; } = new List<PerfilPorPermiso>();

        public virtual List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public virtual List<Permiso> Permisos { get; set; } = new List<Permiso>();


        public Perfil()
        {
            Nombre= string.Empty;
        }
    }
}
