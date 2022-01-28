using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccEventosConfiguration : IEntityTypeConfiguration<AccEventos>
    {
        public void Configure(EntityTypeBuilder<AccEventos> builder)
        {
            builder.ToTable("acc_eventos");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Contexto)
                .HasColumnName("contexto")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.IdAccTipoEvento).HasColumnName("id_acc_tipo_evento");

            builder.Property(e => e.IdAccUsuario).HasColumnName("id_acc_usuario");

            builder.Property(e => e.OcurridoEn)
                .HasColumnName("ocurrido_en")
                .HasColumnType("datetime");

            builder.HasOne(d => d.IdAccTipoEventoNavigation)
                .WithMany(p => p.AccEventos)
                .HasForeignKey(d => d.IdAccTipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_eventos_tipos_eventos");

            builder.HasOne(d => d.IdAccUsuarioNavigation)
                .WithMany(p => p.AccEventos)
                .HasForeignKey(d => d.IdAccUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_eventos_usuarios");
        }
    }
}
