using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPaymentMethodConfiguration : IEntityTypeConfiguration<ScriPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<ScriPaymentMethod> builder)
        {
            builder.ToTable("scri_PaymentMethod");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.IdentificationCode)
            .HasColumnName("IdentificationCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Selected)
            .HasColumnName("Selected")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.CBUTarjetaCredito)
            .HasColumnName("CBUTarjetaCredito")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriPaymentMethod)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_PaymentMethod_Policy");
        }
    }
}
