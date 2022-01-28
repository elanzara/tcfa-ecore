using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AccProgramasAccionesXGrupoConfiguration : IEntityTypeConfiguration<AccProgramasAccionesXGrupo>
    {
        public void Configure(EntityTypeBuilder<AccProgramasAccionesXGrupo> builder)
        {
            builder.ToTable("acc_programas_acciones_x_grupo");

            builder.HasIndex(e => new { e.IdAccGrupo, e.IdProgramaAccion })
                .HasName("uk_programas_acciones_x_grupo")
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
                .HasColumnType("text");

            builder.Property(e => e.IdAccGrupo).HasColumnName("id_acc_grupo");

            builder.Property(e => e.IdProgramaAccion).HasColumnName("id_programa_accion");

            builder.HasOne(d => d.IdAccGrupoNavigation)
                .WithMany(p => p.AccProgramasAccionesXGrupo)
                .HasForeignKey(d => d.IdAccGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_acciones_x_grupo_grupo");

            builder.HasOne(d => d.IdProgramaAccionNavigation)
                .WithMany(p => p.AccProgramasAccionesXGrupo)
                .HasForeignKey(d => d.IdProgramaAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acc_programas_acciones_x_grupo_accion");
        }
    }
}
