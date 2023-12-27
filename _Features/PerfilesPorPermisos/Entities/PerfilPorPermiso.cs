using Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Permisos.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities
{
    public class PerfilPorPermiso
    {                                                                                                 
        public int PerfilId { get; set; }

        public int PermisoId { get; set; }

        public bool Activo { get; set; }
                                                                   
        public int? CreadoPor { get; set; }

        public DateTime? CreadoEl { get; set; }

        public int? ModificadoPor { get; set; }                                                                
                                               

        public DateTime? ModificadoEl { get; set; }
                                         
        public virtual Perfil Perfil { get; set; }
        public virtual Permiso Permiso { get; set; }
    }
}
