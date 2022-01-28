using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriMessagesConfiguration : IEntityTypeConfiguration<ScriMessages>
    {
        public void Configure(EntityTypeBuilder<ScriMessages> builder)
        {
            builder.ToTable("scri_messages");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Id).HasColumnName("IdScriMovimientos");

            builder.Property(e => e.NombreServicio)
                .HasColumnName("NombreServicio")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.VersionServicio)
                .HasColumnName("VersionServicio")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.MessageBeautiful)
                .HasColumnName("MessageBeautiful")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.StackTrace)
                .HasColumnName("StackTrace")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Id).HasColumnName("ErrorLevel");

            builder.HasOne(d => d.IdScriMovimientosNavigation)
            .WithMany(p => p.ScriMessages)
            .HasForeignKey(d => d.IdScriMovimientos)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_mensajes_movimientos");

        }
    }
}