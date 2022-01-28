using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MAccGruposXPerfilConfiguration : IEntityTypeConfiguration<MAccGruposXPerfil>
    {
        public void Configure(EntityTypeBuilder<MAccGruposXPerfil> builder)
        {
            builder.ToTable("m_acc_grupos_x_perfil");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdOrigen).HasColumnName("id_origen");

            builder.Property(e => e.Modifica)
                .IsRequired()
                .HasColumnName("modifica")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.Property(e => e.IdAccGrupo).HasColumnName("id_acc_grupo");

            builder.Property(e => e.IdAccPerfil).HasColumnName("id_acc_perfil");

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

            builder.HasOne(d => d.IdAccGrupoNavigation)
                .WithMany(p => p.MaccGruposXPerfilIdAccGrupoNavigation)
                .HasForeignKey(d => d.IdAccGrupo)
                .HasConstraintName("fk_m_acc_grupos_x_perfil_grupo");

            builder.HasOne(d => d.IdAccPerfilNavigation)
                .WithMany(p => p.MaccGruposXPerfilIdAccPerfilNavigation)
                .HasForeignKey(d => d.IdAccPerfil)
                .HasConstraintName("fk_m_acc_grupos_x_perfil_perfil");

            builder.HasOne(d => d.IdOrigenNavigation)
                .WithMany(p => p.MAccGruposXPerfilIdOrigenNavigation)
                .HasForeignKey(d => d.IdOrigen)
                .HasConstraintName("fk_m_acc_grupos_x_perfil_id_origen");
        }
    }
}
