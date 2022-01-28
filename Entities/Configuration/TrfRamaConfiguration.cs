using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class TrfRamaConfiguration : IEntityTypeConfiguration<TrfRama>
    {
        public void Configure(EntityTypeBuilder<TrfRama> builder)
        {
            builder.ToTable("trf_rama");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdTrfDetallePremio).HasColumnName("id_trf_detalle_premio");
            builder.Property(e => e.Bonificacion).HasColumnType("decimal(12, 2)").HasColumnName("bonificacion");
            builder.Property(e => e.CuotasSociales).HasColumnType("decimal(12, 2)").HasColumnName("cuotas_sociales");
            builder.Property(e => e.DerechoEmiFijo).HasColumnType("decimal(12, 2)").HasColumnName("derecho_emi_fijo");
            builder.Property(e => e.DerechoEmision).HasColumnType("decimal(12, 2)").HasColumnName("derecho_emision");
            builder.Property(e => e.IIBBEmpresa).HasColumnType("decimal(12, 2)").HasColumnName("iibb_empresa");
            builder.Property(e => e.IIBBPercepcion).HasColumnType("decimal(12, 2)").HasColumnName("iibb_percepcion");
            builder.Property(e => e.IIBBRiesgo).HasColumnType("decimal(12, 2)").HasColumnName("iibb_riesgo");
            builder.Property(e => e.IVA).HasColumnType("decimal(12, 2)").HasColumnName("iva");
            builder.Property(e => e.IVAPercepcion).HasColumnType("decimal(12, 2)").HasColumnName("iva_percepcion");
            builder.Property(e => e.IVARNI).HasColumnType("decimal(12, 2)").HasColumnName("iva_rni");
            builder.Property(e => e.ImpInternos).HasColumnType("decimal(12, 2)").HasColumnName("imp_internos");
            builder.Property(e => e.LeyEmergVial).HasColumnType("decimal(12, 2)").HasColumnName("ley_emerg_vial");
            builder.Property(e => e.Premio).HasColumnType("decimal(12, 2)").HasColumnName("premio");
            builder.Property(e => e.Prima).HasColumnType("decimal(12, 2)").HasColumnName("prima");
            builder.Property(e => e.PrimaNeta).HasColumnType("decimal(12, 2)").HasColumnName("prima_neta");
            builder.Property(e => e.rama).HasColumnName("rama");
            builder.Property(e => e.RecargoAdm).HasColumnType("decimal(12, 2)").HasColumnName("recargo_adm");
            builder.Property(e => e.RecargoFin).HasColumnType("decimal(12, 2)").HasColumnName("recargo_fin");
            builder.Property(e => e.RecuperoGastosAsoc).HasColumnType("decimal(12, 2)").HasColumnName("recupero_gastos_asoc");
            builder.Property(e => e.SelladoEmpresa).HasColumnType("decimal(12, 2)").HasColumnName("sellado_empresa");
            builder.Property(e => e.SelladoRiesgo).HasColumnType("decimal(12, 2)").HasColumnName("sellado_riesgo");
            builder.Property(e => e.ServiciosSociales).HasColumnType("decimal(12, 2)").HasColumnName("servicios_sociales");
            builder.Property(e => e.TasaSSN).HasColumnType("decimal(12, 2)").HasColumnName("tasa_ssn");

            builder.HasOne(d => d.IdTrfDetallePremioNavigation)
                .WithMany(p => p.TrfRama)
                .HasForeignKey(d => d.IdTrfDetallePremio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trf_rama");

        }
    }
}
