﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class AllianzComisionesEncConfiguration : IEntityTypeConfiguration<AllianzComisionesEnc>
    {
        public void Configure(EntityTypeBuilder<AllianzComisionesEnc> builder)
        {
            builder.ToTable("allianz_comisiones_enc");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Archivo)
                .HasColumnName("archivo")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Usuario)
                .HasColumnName("usuario")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.FechaProceso)
                .HasColumnName("fecha_proceso")
                .HasColumnType("datetime");

        }
    }
}
