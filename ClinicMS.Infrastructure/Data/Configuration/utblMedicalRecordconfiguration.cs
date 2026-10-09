using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblMedicalRecordconfiguration : IEntityTypeConfiguration<utblMedicalRecord>
  {
    public void Configure(EntityTypeBuilder<utblMedicalRecord> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.AppointmentId)
        .IsRequired();


      builder.HasOne(c => c.Appointment)
        .WithOne(p => p.MedicalRecord)
        .HasForeignKey<utblMedicalRecord>(x => x.AppointmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasIndex(x => x.AppointmentId)
        .IsUnique();

      builder.Property(x => x.Temperature)
        .HasPrecision(5, 2);

      builder.Property(x => x.Height)
        .HasPrecision(5, 2);

      builder.Property(x => x.Weight)
        .HasPrecision(5, 2);

      builder.Property(x => x.Pulse)
        .HasPrecision(5, 2);


      builder.Property(x => x.Diagnosis)
        .IsRequired();

      builder.Property(x => x.CreatedOn)
        .HasDefaultValueSql("GETUTCDATE()");

      builder.HasOne(c => c.Doctor)
        .WithMany(x => x.MedicalRecords)
        .HasForeignKey(x => x.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasQueryFilter(m => m.Doctor!.IsActive);
    }
  }
}
