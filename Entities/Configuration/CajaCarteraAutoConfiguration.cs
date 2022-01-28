using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eCore.Entities.Configuration
{
    public class CajaCarteraAutoConfiguration : IEntityTypeConfiguration<CajaCarteraAuto>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraAuto> builder)
        {
            builder.ToTable("caja_cartera_auto");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");

            builder.Property(e => e.Patente)
                    .HasColumnName("patente")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Marca).HasColumnName("Marca");

            builder.Property(e => e.DescMarca)
                    .HasColumnName("desc_marca")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Modelo).HasColumnName("modelo");
            builder.Property(e => e.SumaAsegurada).HasColumnType("decimal(10)").HasColumnName("suma_asegurada");
            builder.Property(e => e.TipoVehiculo).HasColumnName("tipo_vehiculo");
            builder.Property(e => e.UsoVehiculo).HasColumnName("uso_vehiculo");
            builder.Property(e => e.ClaseVehiculo).HasColumnName("clase_vehiculo");

            builder.Property(e => e.Motor)
                    .HasColumnName("motor")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Chasis)
                    .HasColumnName("chasis")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.McaCeroKm)
                    .HasColumnName("mca_cero_km")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.CodInfoauto).HasColumnName("cod_infoauto");

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                    .WithMany(p => p.CajaCarteraAuto)
                    .HasForeignKey(d => d.IdCajaCarteraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_caja_cartera_auto");

        }
        }
}
