using Academia.SistemaGestionInventario.WApi._Features.SalidasInventario.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class SalidaInventarioMap : IEntityTypeConfiguration<SalidaInventario>
    {

        public void Configure(EntityTypeBuilder<SalidaInventario> builder)
        {
            builder.ToTable("SalidasInventario");
            builder.HasKey(e => e.SalidaInventarioId);
            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioId");
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioId");
            builder.Property(e => e.EstadoId).HasColumnName("EstadoId");
            builder.Property(e => e.FechaSalida).HasColumnType("date").HasColumnName("Fecha");
            builder.Property(e => e.SucursalId).HasColumnName("SucursalId");
            builder.Property(e => e.Total).HasColumnType("decimal(10,2)").HasColumnName("Total");
            builder.Property(e => e.FechaRecibido).HasColumnType("date").IsRequired(false).HasColumnName("FechaRecibido");
            builder.Property(e => e.UsuarioIdRecibe).HasColumnName("UsuarioIdRecibe").IsRequired(false);
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");

            builder.HasOne(d => d.Usuario).WithMany(p => p.SalidaInventario)
                .HasForeignKey(d => d.UsuarioId);


            builder.HasOne(d => d.Estado).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.EstadoId);

            builder.HasOne(d => d.Sucursal).WithMany(p => p.SalidaInventario)
                .HasForeignKey(d => d.SucursalId);

        }

    }
}
