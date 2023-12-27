using Academia.SistemaGestionInventario.WApi._Features.Productos.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(e => e.ProductoId);
            builder.Property(e => e.ProductoId).HasColumnName("ProductoId");
            builder.Property(e => e.Nombre).HasMaxLength(255).HasColumnName("Nombre").IsRequired();
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");
        }

    }
}
