using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccGruposXPerfilConfiguration : IEntityTypeConfiguration<AccGruposXPerfil>
    {
        public void Configure(EntityTypeBuilder<AccGruposXPerfil> builder)
        {
            builder.ToTable("acc_grupos_x_perfil");

            builder.HasIndex(e => new { e.IdAccPerfil, e.IdAccGrupo })
                .HasName("uk_grupos_x_perfil")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AutorizadoEn)
                .HasColumnName("autorizado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.AutorizadoPor)
                .HasColumnName("autorizado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.CreadoPor)
                .IsRequired()
                .HasColumnName("creado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IdAccGrupo).HasColumnName("id_acc_grupo");

            builder.Property(e => e.IdAccPerfil).HasColumnName("id_acc_perfil");

            builder.HasOne(d => d.IdAccGrupoNavigation)
                .WithMany(p => p.AccGruposXPerfil)
                .HasForeignKey(d => d.IdAccGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_grupos_x_perfil_modulos");

            builder.HasOne(d => d.IdAccPerfilNavigation)
                .WithMany(p => p.AccGruposXPerfil)
                .HasForeignKey(d => d.IdAccPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_grupos_x_perfil_perfiles");
        }
    }
}
