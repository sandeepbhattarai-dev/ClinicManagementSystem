using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblPrescriptionItemConfiguration : IEntityTypeConfiguration<utblPrescriptionItem>
  {
    public void Configure(EntityTypeBuilder<utblPrescriptionItem> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasOne(c => c.Prescription)
        .WithMany(p => p.Items)
        .HasForeignKey(c => c.PrescriptionId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.Property(x => x.MedicationName)
        .IsRequired();

      //builder.Property(x => x.Dosage)
      //  .IsRequired();

      //builder.Property(x => x.Frequency)
      //  .IsRequired();

      builder.Property(x => x.Instructions)
        .HasMaxLength(500);

      builder.HasQueryFilter(s => s.Prescription!.Patient!.IsActive);
    }
  }
}
