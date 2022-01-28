using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasAccionesConfiguration : IEntityTypeConfiguration<AccProgramasAcciones>
    {
        public void Configure(EntityTypeBuilder<AccProgramasAcciones> builder)
        {
            builder.ToTable("acc_programas_acciones");

            builder.HasIndex(e => new { e.IdAccPrograma, e.IdAccAccion, e.Origen })
                .HasName("uk_programas_acciones")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdAccAccion).HasColumnName("id_acc_accion");

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

            builder.Property(e => e.Origen)
                .HasColumnName("origen")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IdAccPrograma).HasColumnName("id_acc_programa");

            builder.HasOne(d => d.IdAccAccionNavigation)
                .WithMany(p => p.AccProgramasAcciones)
                .HasForeignKey(d => d.IdAccAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_acciones_acciones");

            builder.HasOne(d => d.IdAccProgramaNavigation)
                .WithMany(p => p.AccProgramasAcciones)
                .HasForeignKey(d => d.IdAccPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_acciones_programas");
        }
    }
}
