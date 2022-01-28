using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapImpuestosConfiguration : IEntityTypeConfiguration<MapImpuestos>
    {
        public void Configure(EntityTypeBuilder<MapImpuestos> builder)
        {
            builder.ToTable("map_impuestos");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.primaComisionable).HasColumnType("decimal(8, 2)").HasColumnName("primaComisionable");
            builder.Property(e => e.primaNoComisionable).HasColumnType("decimal(8, 2)").HasColumnName("primaNoComisionable");
            builder.Property(e => e.derEmis).HasColumnType("decimal(8, 2)").HasColumnName("derEmis");
            builder.Property(e => e.recAdmin).HasColumnType("decimal(8, 2)").HasColumnName("recAdmin");
            builder.Property(e => e.recFinan).HasColumnType("decimal(8, 2)").HasColumnName("recFinan");
            builder.Property(e => e.bonificaciones).HasColumnType("decimal(8, 2)").HasColumnName("bonificaciones");
            builder.Property(e => e.bonifAdic).HasColumnType("decimal(8, 2)").HasColumnName("bonifAdic");
            builder.Property(e => e.otrosImptos).HasColumnType("decimal(8, 2)").HasColumnName("otrosImptos");
            builder.Property(e => e.servSociales).HasColumnType("decimal(8, 2)").HasColumnName("servSociales");
            builder.Property(e => e.imptosInternos).HasColumnType("decimal(8, 2)").HasColumnName("imptosInternos");
            builder.Property(e => e.ingBrutos).HasColumnType("decimal(8, 2)").HasColumnName("ingBrutos");
            builder.Property(e => e.premio).HasColumnType("decimal(8, 2)").HasColumnName("premio");
            builder.Property(e => e.porComision).HasColumnName("porComision");

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapImpuestos)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_impuestos_novedades");
        }
    }
}