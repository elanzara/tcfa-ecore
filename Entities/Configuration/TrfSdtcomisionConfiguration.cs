using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class TrfSdtcomisionConfiguration : IEntityTypeConfiguration<TrfSdtcomision>
    {
        public void Configure(EntityTypeBuilder<TrfSdtcomision> builder)
        {
            builder.ToTable("trf_sdtcomision");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdTrfNovedades).HasColumnName("id_trf_novedades");
            builder.Property(e => e.Monto).HasColumnType("decimal(12, 2)").HasColumnName("monto");
            builder.Property(e => e.NIVC).HasColumnName("nivc");
            builder.Property(e => e.NIVT).HasColumnName("nivt");
            builder.Property(e => e.Porcentaje).HasColumnType("decimal(12, 2)").HasColumnName("porcentaje");
            builder.Property(e => e.Rama).HasColumnName("rama");

            builder.HasOne(d => d.IdTrfNovedadesNavigation)
            .WithMany(p => p.TrfSdtcomision)
            .HasForeignKey(d => d.IdTrfNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_sdtcomision_novedades");
        }
    }
}
