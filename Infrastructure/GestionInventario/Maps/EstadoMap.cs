using Academia.SistemaGestionInventario.WApi._Features.Estados.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados");
            builder.HasKey(e => e.EstadoId);
            builder.Property(e => e.EstadoId).HasColumnName("EstadoID").IsRequired();
            builder.Property(e => e.EstadoNombre).HasMaxLength(50).IsRequired().HasColumnName("EstadoNombre");
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");
        }
    }
}
