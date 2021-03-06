using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MAccPerfilesConfiguration : IEntityTypeConfiguration<MAccPerfiles>
    {
        public void Configure(EntityTypeBuilder<MAccPerfiles> builder)
        {
            builder.ToTable("m_acc_perfiles");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AutorizadoEn)
                .HasColumnName("autorizado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.AutorizadoPor)
                .HasColumnName("autorizado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasColumnName("codigo")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CreadoEn)
                .HasColumnName("creado_en")
                .HasColumnType("datetime");

            builder.Property(e => e.CreadoPor)
                .IsRequired()
                .HasColumnName("creado_por")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Icono)
                .HasColumnName("icono")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.IdOrigen).HasColumnName("id_origen");

            builder.Property(e => e.Modifica)
                .IsRequired()
                .HasColumnName("modifica")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.HasOne(d => d.IdOrigenNavigation)
                .WithMany(p => p.MAccPerfiles)
                .HasForeignKey(d => d.IdOrigen)
                .HasConstraintName("fk_m_acc_perfiles");
        }
    }
}
