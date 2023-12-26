using Academia.SistemaGestionInventario.WApi._Features.ProductosLotes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class ProductoLoteMap : IEntityTypeConfiguration<ProductosLote>
    {

        public void Configure(EntityTypeBuilder<ProductosLote> builder)
        {
            builder.ToTable("ProductosLotes");
            builder.HasKey(e => e.LoteId);
            builder.Property(e => e.LoteId).HasColumnName("LoteID").IsRequired();
            builder.Property(e => e.CantidadInicial).HasColumnName("CantidadInicial").IsRequired();
            builder.Property(e => e.CostoUnidad).HasColumnName("CostoUnidad").HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.FechaVencimiento).HasColumnType("date").HasColumnName("FechaVencimiento").IsRequired();
            builder.Property(e => e.Inventario).HasColumnName("Inventario").IsRequired();
            builder.Property(e => e.ProductoId).HasColumnName("ProductoID").IsRequired();
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");
            builder.HasOne(d => d.Producto).WithMany(p => p.ProductosLotes).HasForeignKey(d => d.ProductoId);
        }

    }
}
