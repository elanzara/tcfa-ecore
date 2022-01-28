using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class AllianzComisionesDetConfiguration : IEntityTypeConfiguration<AllianzComisionesDet>
    {
        public void Configure(EntityTypeBuilder<AllianzComisionesDet> builder)
        {
            builder.ToTable("allianz_comisiones_det");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdAllianzComisionesEnc).HasColumnName("id_allianz_comisiones_enc");

            builder.Property(e => e.Organizador)
                .HasColumnName("organizador")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Productor)
                .HasColumnName("productor")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Tipo)
                .HasColumnName("tipo")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("datetime");

            builder.Property(e => e.Seccion)
                .HasColumnName("seccion")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.NroPoliza)
                .HasColumnName("nro_poliza")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Endoso).HasColumnName("endoso");

            builder.Property(e => e.Asegurado)
                .HasColumnName("asegurado")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Mda)
                .HasColumnName("mda")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.TipoCambio).HasColumnName("tipo_cambio");

            builder.Property(e => e.Premio).HasColumnType("decimal(12, 2)").HasColumnName("premio");

            builder.Property(e => e.Prima).HasColumnType("decimal(12, 2)").HasColumnName("prima");

            builder.Property(e => e.ComisionesDevengadas).HasColumnType("decimal(12, 2)").HasColumnName("comisiones_devengadas");

            builder.Property(e => e.ComisionesDevengadasPesos).HasColumnType("decimal(12, 2)").HasColumnName("comisiones_devengadas_pesos");

            builder.Property(e => e.OSSEG).HasColumnType("decimal(12, 2)").HasColumnName("osseg");

            builder.Property(e => e.IBAgente).HasColumnType("decimal(12, 2)").HasColumnName("ib_agente");

            builder.Property(e => e.IBRiesgo).HasColumnType("decimal(12, 2)").HasColumnName("ib_riesgo");

            builder.Property(e => e.ProvinciaRiesgo)
                .HasColumnName("Provincia_riesgo")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.NetoAcreditado).HasColumnType("decimal(12, 2)").HasColumnName("neto_acreditado");

            builder.Property(e => e.NetoAcreditadoPesos).HasColumnType("decimal(12, 2)").HasColumnName("neto_acreditado_pesos");

            builder.Property(e => e.FPago)
                .HasColumnName("f_pago")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.IdAllianzComisionesEncNavigation)
                .WithMany(p => p.AllianzComisionesDet)
                .HasForeignKey(d => d.IdAllianzComisionesEnc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_allianz_comisiones_det");

        }
    }
}
