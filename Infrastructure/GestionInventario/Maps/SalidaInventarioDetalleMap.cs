using Academia.SistemaGestionInventario.WApi._Features.SalidasInventarioDetalle.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class SalidaInventarioDetalleMap : IEntityTypeConfiguration<SalidaInventarioDetalle>
    {
        public void Configure(EntityTypeBuilder<SalidaInventarioDetalle> builder)
        {
            builder.ToTable("SalidasInventarioDetalle");
            builder.HasKey(e => e.SalidaDetalleId);
            builder.Property(e => e.SalidaDetalleId).HasColumnName("SalidaDetalleId").IsRequired();
            builder.Property(e => e.LoteId).HasColumnName("LoteId").IsRequired();
            builder.Property(e => e.CantidadProducto).HasColumnName("CantidadProducto").IsRequired();

            builder.Property(e => e.SalidaInventarioId).HasColumnName("SalidaInventarioId").IsRequired();
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");

            builder.HasOne(d => d.Lote).WithMany(p => p.SalidaInventarioDetalle)
                .HasForeignKey(d => d.LoteId);

            builder.HasOne(d => d.SalidaInventario).WithMany(p => p.SalidaInventarioDetalle)
                .HasForeignKey(d => d.SalidaInventarioId);

        }

    }
}
