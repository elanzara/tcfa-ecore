using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccUsuariosSesionesConfiguration : IEntityTypeConfiguration<AccUsuariosSesiones>
    {
        public void Configure(EntityTypeBuilder<AccUsuariosSesiones> builder)
        {
            builder.ToTable("acc_usuarios_sesiones");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.Eventos)
                .HasColumnName("eventos")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.Property(e => e.FinalizaEn)
                .HasColumnName("finaliza_en")
                .HasColumnType("datetime");

            builder.Property(e => e.UltimaConexion)
                .HasColumnName("ultima_conexion")
                .HasColumnType("datetime");

            builder.Property(e => e.IdAccUsuario).HasColumnName("id_acc_usuario");

            builder.Property(e => e.Token)
                .IsRequired()
                .HasColumnName("token")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.IdAccUsuarioNavigation)
                .WithMany(p => p.AccUsuariosSesiones)
                .HasForeignKey(d => d.IdAccUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_usuarios_sesiones");
        }
    }
}
