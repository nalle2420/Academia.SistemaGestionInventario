using Academia.SistemaGestionInventario.WApi._Common;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Dtos;
using FluentValidation;

namespace Academia.SistemaGestionInventario.WApi._Features.Permisos.Dtos
{
    public class PermisoDto
    {
        public int? PermisoId { get; set; }

        public string? PermisoNombre { get; set; }

        public PermisoDto()
        {
            PermisoNombre = string.Empty;
        }
    }


}
