using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasXModulosConfiguration : IEntityTypeConfiguration<AccProgramasXModulos>
    {
        public void Configure(EntityTypeBuilder<AccProgramasXModulos> builder)
        {
            builder.ToTable("acc_programas_x_modulos");

            builder.HasIndex(e => new { e.IdAccPrograma, e.IdAccModulo })
                .HasName("uk_programas_x_modulos")
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

            builder.Property(e => e.Icono)
                .HasColumnName("icono")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.IdAccModulo).HasColumnName("id_acc_modulo");

            builder.Property(e => e.IdAccPrograma).HasColumnName("id_acc_programa");

            builder.HasOne(d => d.IdAccModuloNavigation)
                .WithMany(p => p.AccProgramasXModulos)
                .HasForeignKey(d => d.IdAccModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_modulos_programas_x_modulos");

            builder.HasOne(d => d.IdAccProgramaNavigation)
                .WithMany(p => p.AccProgramasXModulos)
                .HasForeignKey(d => d.IdAccPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_x_modulos");
        }
    }
}
