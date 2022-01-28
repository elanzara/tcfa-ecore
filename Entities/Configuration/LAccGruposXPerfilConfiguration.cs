using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class LAccGruposXPerfilConfiguration : IEntityTypeConfiguration<LAccGruposXPerfil>
    {
        public void Configure(EntityTypeBuilder<LAccGruposXPerfil> builder)
        {
            builder.ToTable("l_acc_grupos_x_perfil");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdOrigen).HasColumnName("id_origen");

            builder.Property(e => e.Accionsql)
                .IsRequired()
                .HasColumnName("accionsql")
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
        }
    }
}
