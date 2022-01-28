using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class TrfDetallePremioConfiguration : IEntityTypeConfiguration<TrfDetallePremio>
    {
        public void Configure(EntityTypeBuilder<TrfDetallePremio> builder)
        {
            builder.ToTable("trf_detalle_premio");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdTrfNovedades).HasColumnName("id_trf_novedades");
            builder.Property(e => e.Premio).HasColumnType("decimal(12, 2)").HasColumnName("premio");

            builder.HasOne(d => d.IdTrfNovedadesNavigation)
            .WithMany(p => p.TrfDetallePremio)
            .HasForeignKey(d => d.IdTrfNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_detalle_premio_novedades");
        }
    }
}
