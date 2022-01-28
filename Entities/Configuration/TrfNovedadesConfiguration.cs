using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class TrfNovedadesConfiguration : IEntityTypeConfiguration<TrfNovedades>
    {
        public void Configure(EntityTypeBuilder<TrfNovedades> builder)
        {
            builder.ToTable("trf_novedades");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Articulo).HasColumnName("articulo");

            builder.Property(e => e.ArticuloAnt).HasColumnName("articulo_ant");

            builder.Property(e => e.CUIT)
                .HasColumnName("cuit")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Certificado)
                .HasColumnName("certificado")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.CertificadoAnt)
                .HasColumnName("certificado_ant")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.CodigoPostal).HasColumnName("codigo_postal");

            builder.Property(e => e.CondicionIVA)
                .HasColumnName("condicion_iva")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.CondicionIVAN).HasColumnName("condicion_ivan");

            builder.Property(e => e.DocNumero)
                .HasColumnName("doc_numero")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.DocTipo).HasColumnName("doc_tipo");

            builder.Property(e => e.Domicilio)
                .HasColumnName("domicilio")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Empresa)
                .HasColumnName("empresa")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.EmpresaAnt)
                .HasColumnName("empresa_ant")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.EstadoPoliza)
                .HasColumnName("estado_poliza")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.EstadoPolizaN)
                .HasColumnName("estado_poliza_n")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Moneda)
                .HasColumnName("moneda")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.RazonSocial)
                .HasColumnName("razon_social")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.SubCodigoPostal).HasColumnName("sub_codigo_postal");

            builder.Property(e => e.Sucursal)
                .HasColumnName("sucursal")
                .HasMaxLength(255)
                .IsUnicode(false);

            
            builder.Property(e => e.SucursalAnt)
                .HasColumnName("Sucursal_ant")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Suplemento).HasColumnName("suplemento");

            builder.Property(e => e.Telefono)
                .HasColumnName("telefono")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.TelefonoParticular)
                .HasColumnName("telefono_particular")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.VigenciaDesde)
                .HasColumnName("vigencia_desde")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.VigenciaHasta)
                .HasColumnName("vigencia_hasta")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.codigo_productor)
                .HasColumnName("codigo_productor")
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
