using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriReasonCancelDTOConfiguration : IEntityTypeConfiguration<ScriReasonCancelDTO>
    {
        public void Configure(EntityTypeBuilder<ScriReasonCancelDTO> builder)
        {
            builder.ToTable("scri_ReasonCancelDTO");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.ReasonCode)
            .HasColumnName("ReasonCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.ReasonDescrip)
            .HasColumnName("ReasonDescrip")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.CancellationDate)
            .HasColumnName("CancellationDate")
            .HasColumnType("datetime");

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriReasonCancelDTO)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_ReasonCancelDTO_Policy");
        }
    }
}
