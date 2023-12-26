using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;

namespace Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        public string Identidad { get; set; }


        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public bool Activo { get; set; }

        public int CreadoPor { get; set; }

        public DateTime CreadoEl { get; set; }

        public int ModificadoPor { get; set; }

        public DateTime ModificadoEl { get; set; }

        public virtual List<Usuario> Usuarios { get; set; } = new List<Usuario>();


        public Empleado()
        {
            Identidad = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
        }
    }
}
