using Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities;
using Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Permisos.Entities
{
    public class Permiso
    {
        public int PermisoId { get; set; }

        public string PermisoNombre { get; set; }

        public bool Activo { get; set; }

        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? ModificadoEl { get; set; }

        public virtual List<Perfil> Perfil { get; set; } = new List<Perfil>();
        public virtual List<PerfilPorPermiso> PerfilPorPermiso { get; set; } = new List<PerfilPorPermiso>();

        public Permiso()
        {
            PermisoNombre = string.Empty;
        }

    }
}
