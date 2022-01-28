using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasFavoritosXUsuarioConfiguration : IEntityTypeConfiguration<AccProgramasFavoritosXUsuario>
    {
        public void Configure(EntityTypeBuilder<AccProgramasFavoritosXUsuario> builder)
        {
            builder.ToTable("acc_programas_favoritos_x_usuario");

            builder.HasIndex(e => new { e.IdAccPrograma, e.IdAccUsuario })
                .HasName("uk_acc_programas_favoritos_x_usuario")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.CreadoPor)
                .IsRequired()
                .HasColumnName("creado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IdAccPrograma).HasColumnName("id_acc_programa");

            builder.Property(e => e.IdAccUsuario).HasColumnName("id_acc_usuario");

            builder.HasOne(d => d.IdAccUsuarioNavigation)
                                                .WithMany(p => p.AccProgramasFavoritosXUsuario)
                                                .HasForeignKey(d => d.IdAccUsuario)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("fk_acc_programas_favoritos_x_usuario_usr");

            builder.HasOne(d => d.IdAccProgramaNavigation)
                .WithMany(p => p.AccProgramasFavoritosXUsuario)
                .HasForeignKey(d => d.IdAccPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_favoritos_x_usuario_prog");
        }
    }
}
