using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasRecientesXUsuarioConfiguration : IEntityTypeConfiguration<AccProgramasRecientesXUsuario>
    {
        public void Configure(EntityTypeBuilder<AccProgramasRecientesXUsuario> builder)
        {
            builder.ToTable("acc_programas_recientes_x_usuario");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("datetime");

            builder.Property(e => e.IdAccPrograma).HasColumnName("id_acc_programa");

            builder.Property(e => e.IdAccUsuario).HasColumnName("id_acc_usuario");

            builder.HasOne(d => d.IdAccUsuarioNavigation)
                                                .WithMany(p => p.AccProgramasRecientesXUsuario)
                                                .HasForeignKey(d => d.IdAccUsuario)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("fk_acc_programas_recientes_x_usuario_usr");

            builder.HasOne(d => d.IdAccProgramaNavigation)
                .WithMany(p => p.AccProgramasRecientesXUsuario)
                .HasForeignKey(d => d.IdAccPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_recientes_x_usuario_prog");
        }
    }
}
