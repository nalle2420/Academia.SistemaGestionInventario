using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Estados.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Perfiles.Entities;
using Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Permisos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Sucursales.Entities;
using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario
{
    public class GestionInventarioDbContext: DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<ProductosLote> ProductosLotes { get; set; }

        public DbSet<Perfil> Perfil { get; set; }

        public DbSet<PerfilPorPermiso> PerfilPorPermiso { get; set; }

        public DbSet<SalidaInventario> SalidasInventarios { get; set; }

        public DbSet<SalidaInventarioDetalle> SalidasInventarioDetalles { get; set; }

        public DbSet<Sucursal> Sucursales { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public GestionInventarioDbContext(DbContextOptions<GestionInventarioDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
            modelBuilder.ApplyConfiguration(new PermisoMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new ProductoMap());
            modelBuilder.ApplyConfiguration(new ProductoLoteMap());
            modelBuilder.ApplyConfiguration(new PerfilPorPermisoMap());
            modelBuilder.ApplyConfiguration(new SalidaInventarioMap());
            modelBuilder.ApplyConfiguration(new SalidaInventarioDetalleMap());
            modelBuilder.ApplyConfiguration(new SucursalMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

        }
    }
}
