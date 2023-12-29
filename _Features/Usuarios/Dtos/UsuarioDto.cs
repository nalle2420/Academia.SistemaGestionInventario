namespace Academia.SistemaGestionInventario.WApi._Features.Usuarios.Dtos
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public int EmpleadoId { get; set; }

        public int PerfilId { get; set; }

        public bool Activo { get; set; }

        public UsuarioDto()
        {
            Nombre = string.Empty;
        }
    }
}
