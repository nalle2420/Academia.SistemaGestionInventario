using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Academia.SistemaGestionInventario.WApi._Features.PerfilesPorPermisos.Entities;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class PerfilPorPermisoMap : IEntityTypeConfiguration<PerfilPorPermiso>
    {

        public void Configure(EntityTypeBuilder<PerfilPorPermiso> builder)
        {
            builder.ToTable("PerfilesPorPermisos");
            builder.HasKey(e => new { e.PermisoId, e.PerfilId });
            builder.Property(e => e.PermisoId).HasColumnName("PermisoId");
            builder.Property(e => e.PerfilId).HasColumnName("PerfilId");
            builder.Property(e => e.Activo).HasColumnName("Activo").IsRequired();
            builder.Property(e => e.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(e => e.CreadoEl).HasColumnName("CreadoEl");
            builder.Property(e => e.ModificadoPor).HasColumnName("ModificadoPor");
            builder.Property(e => e.ModificadoEl).HasColumnName("ModificadoEl");

            builder.HasOne(se => se.Permiso)
                .WithMany(e => e.PerfilPorPermiso)
                .HasForeignKey(se => se.PermisoId);

            builder.HasOne(se => se.Perfil)
               .WithMany(e => e.PerfilPorPermiso)
               .HasForeignKey(se => se.PerfilId);

        }

    }
}
