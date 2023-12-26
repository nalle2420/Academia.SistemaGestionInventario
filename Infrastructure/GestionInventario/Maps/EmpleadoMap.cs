using Academia.SistemaGestionInventario.WApi._Features.Empleados.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class EmpleadoMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados");
            builder.HasKey(e => e.EmpleadoId);
            builder.Property(x => x.EmpleadoId).HasColumnName("EmpleadoId").IsRequired();
            builder.Property(e => e.Identidad).HasColumnName("Identidad").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Nombre).HasColumnName("Nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.Apellido).HasColumnName("Apellido").HasMaxLength(50).IsRequired();
            builder.Property(e => e.Direccion).HasColumnName("Direccion").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Telefono).HasColumnName("Telefono").HasMaxLength(20).IsRequired();
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");

        }
    }
}
