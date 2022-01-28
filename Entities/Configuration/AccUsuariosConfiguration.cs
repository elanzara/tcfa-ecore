﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccUsuariosConfiguration : IEntityTypeConfiguration<AccUsuarios>
    {
        public void Configure(EntityTypeBuilder<AccUsuarios> builder)
        {
            builder.ToTable("acc_usuarios");

            builder.HasIndex(e => e.AdCuenta)
                .HasName("uk_ad_cuenta")
                .IsUnique();

            builder.HasIndex(e => e.SecurityIdentifier)
                .HasName("uk_security_identifier")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AdCuenta)
                .IsRequired()
                .HasColumnName("ad_cuenta")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Apellido)
                .IsRequired()
                .HasColumnName("apellido")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.AutorizadoEn)
                .HasColumnName("autorizado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.AutorizadoPor)
                .HasColumnName("autorizado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Bloqueado).HasColumnName("bloqueado");

            builder.Property(e => e.Sucursal)
                .IsRequired()
                .HasColumnName("sucursal")
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.UltimaConexion)
                .HasColumnName("ultima_conexion")
                .HasColumnType("datetime");

            builder.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.CreadoPor)
                .IsRequired()
                .HasColumnName("creado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IdAccPerfil).HasColumnName("id_acc_perfil");

            builder.Property(e => e.MaxCantidadConexiones).HasColumnName("max_cantidad_conexiones");

            builder.Property(e => e.Nombres)
                .IsRequired()
                .HasColumnName("nombres")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.SecurityIdentifier)
                .IsRequired()
                .HasColumnName("security_identifier")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.IdAccPerfilNavigation)
                .WithMany(p => p.AccUsuarios)
                .HasForeignKey(d => d.IdAccPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_usuarios_perfiles");
        }
    }
}