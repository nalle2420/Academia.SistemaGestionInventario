﻿using Academia.SistemaGestionInventario.WApi._Features.Usuarios.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(e => e.UsuarioId);
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioId");
            builder.Property(e => e.Clave).HasMaxLength(100).IsRequired().HasColumnName("Clave");
            builder.Property(e => e.EmpleadoId).HasColumnName("EmpleadoId");
            builder.Property(e => e.PerfilId).HasColumnName("PerfilId");

            builder.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("Nombre");

            builder.HasOne(d => d.Empleado).WithMany(p => p.Usuarios).HasForeignKey(d => d.EmpleadoId);
            builder.HasOne(d => d.Perfil).WithMany(p => p.Usuarios).HasForeignKey(d => d.PerfilId);


        }

    }
}
