using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasXUsuarioConfiguration : IEntityTypeConfiguration<AccProgramasXUsuario>
    {
        public void Configure(EntityTypeBuilder<AccProgramasXUsuario> builder)
        {
            builder.ToTable("acc_programas_x_usuario");

            builder.HasIndex(e => new { e.IdAccPrograma, e.IdAccUsuario })
                .HasName("uk_programas_x_usuario")
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

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasColumnName("tipo")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.HasOne(d => d.IdAccUsuarioNavigation)
                                .WithMany(p => p.AccProgramasXUsuario)
                                .HasForeignKey(d => d.IdAccUsuario)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("fk_acc_programas_x_usuario_usr");

            builder.HasOne(d => d.IdAccProgramaNavigation)
                .WithMany(p => p.AccProgramasXUsuario)
                .HasForeignKey(d => d.IdAccPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_x_usuario_prog");
        }
    }
}
