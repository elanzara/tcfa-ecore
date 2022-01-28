using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class AllianzCarteraDetConfiguration : IEntityTypeConfiguration<AllianzCarteraDet>
    {
        public void Configure(EntityTypeBuilder<AllianzCarteraDet> builder)
        {
            builder.ToTable("allianz_cartera_det");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdAllianzCarteraEnc).HasColumnName("id_allianz_cartera_enc");

            builder.Property(e => e.Productor)
                .HasColumnName("productor")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Organizador)
                .HasColumnName("organizador")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Seccion)
                .HasColumnName("seccion")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Poliza)
                .HasColumnName("poliza")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Endoso).HasColumnName("endoso");

            builder.Property(e => e.ClaseEndoso)
                .HasColumnName("clase_endoso")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.NombreDelAsegurado)
                .HasColumnName("nombre_del_asegurado")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.FecEmision)
                .HasColumnName("fec_emision")
                .HasColumnType("datetime");

            builder.Property(e => e.FechaDesdeVigencia)
                .HasColumnName("fecha_desde_vigencia")
                .HasColumnType("datetime");

            builder.Property(e => e.FechaHastaVigencia)
                .HasColumnName("fecha_hasta_vigencia")
                .HasColumnType("datetime");

            builder.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Moneda)
                .HasColumnName("moneda")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.TotalPrima).HasColumnType("decimal(12, 2)").HasColumnName("total_prima");

            builder.Property(e => e.ComisionOrg).HasColumnType("decimal(12, 2)").HasColumnName("comision_org");

            builder.Property(e => e.ComisionProd).HasColumnType("decimal(12, 2)").HasColumnName("comision_prod");

            builder.Property(e => e.TotalPremio).HasColumnType("decimal(12, 2)").HasColumnName("total_premio");

            builder.Property(e => e.TotalPagado).HasColumnType("decimal(12, 2)").HasColumnName("total_pagado");

            builder.Property(e => e.Saldo).HasColumnType("decimal(12, 2)").HasColumnName("saldo");

            builder.Property(e => e.CantSiniestros).HasColumnName("cant_siniestros");

            builder.Property(e => e.CantDenuncias).HasColumnName("cant_denuncias");

            builder.Property(e => e.TipoDeDocumento)
                .HasColumnName("tipo_de_documento")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.NumeroDeDocumento)
                .HasColumnName("numero_de_documento")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.CantCuotas).HasColumnName("cant_cuotas");

            builder.Property(e => e.FormaDeCobro)
                .HasColumnName("forma_de_cobro")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.TipoOperacion)
                .HasColumnName("tipo_operacion")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.DerechoEmision).HasColumnType("decimal(12, 2)").HasColumnName("derecho_emision");

            builder.Property(e => e.GastosFinanc).HasColumnType("decimal(12, 2)").HasColumnName("gastos_financ");

            builder.Property(e => e.Iva).HasColumnType("decimal(12, 2)").HasColumnName("iva");

            builder.Property(e => e.Sellos).HasColumnType("decimal(12, 2)").HasColumnName("sellos");

            builder.Property(e => e.GastosAdm).HasColumnType("decimal(12, 2)").HasColumnName("gastos_adm");

            builder.Property(e => e.SumaAsegurada).HasColumnType("decimal(12, 2)").HasColumnName("suma_asegurada");

            builder.Property(e => e.ValorDeReferencia).HasColumnType("decimal(12, 2)").HasColumnName("valor_de_referencia");

            builder.Property(e => e.TipoPoliza)
                .HasColumnName("tipo_poliza")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Cuatrimestre)
                .HasColumnName("cuatrimestre")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.EstadoSolicitud)
                .HasColumnName("estado_solicitud")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.FechaDespImp)
                .HasColumnName("fecha_desp_imp")
                .HasColumnType("datetime");

            builder.Property(e => e.Propuesta)
                .HasColumnName("propuesta")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Linea)
                .HasColumnName("linea")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.PolizaRenovada)
                .HasColumnName("poliza_renovada")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.CantCuotas2).HasColumnName("cant_cuotas2");

            builder.Property(e => e.Porc1erCuota).HasColumnType("decimal(12, 2)").HasColumnName("porc_1er_cuota");

            builder.Property(e => e.Venc1eraCuota).HasColumnName("venc_1era_cuota");

            builder.Property(e => e.PlanPago)
                .HasColumnName("plan_pago")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.NroInterno).HasColumnType("decimal(12, 2)").HasColumnName("nro_interno");

            builder.Property(e => e.FechaVtoPoliza)
                .HasColumnName("fecha_vto_poliza")
                .HasColumnType("datetime");

            builder.Property(e => e.Patente)
                .HasColumnName("patente")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Marca)
                .HasColumnName("marca")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Modelo)
                .HasColumnName("modelo")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Motor)
                .HasColumnName("motor")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Chasis)
                .HasColumnName("chasis")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Uso)
                .HasColumnName("uso")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Cobertura)
                .HasColumnName("cobertura")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.SumaAsegurada2).HasColumnType("decimal(12, 2)").HasColumnName("suma_asegurada2");

            builder.Property(e => e.ValorDeReferencia2).HasColumnType("decimal(12, 2)").HasColumnName("valor_de_referencia2");

            builder.Property(e => e.Item).HasColumnName("item");

            builder.Property(e => e.Infoauto).HasColumnType("decimal(12, 2)").HasColumnName("infoauto");

            builder.Property(e => e.FechaFinPrestamo)
                .HasColumnName("fecha_fin_prestamo")
                .HasColumnType("datetime");

            builder.HasOne(d => d.IdAllianzCarteraEncNavigation)
                .WithMany(p => p.AllianzCarteraDet)
                .HasForeignKey(d => d.IdAllianzCarteraEnc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_allianz_cartera_det");

        }
    }
}
