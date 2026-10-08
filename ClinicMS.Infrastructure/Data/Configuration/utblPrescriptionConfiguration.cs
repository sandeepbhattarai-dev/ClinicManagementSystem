using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblPrescriptionConfiguration : IEntityTypeConfiguration<utblPrescription>
  {
    public void Configure(EntityTypeBuilder<utblPrescription> builder)
    {
      builder.HasKey(t => t.Id);

      builder.HasIndex(t => t.AppointmentId).IsUnique();

      builder.HasOne(c => c.Appointment)
        .WithOne(p => p.Prescription)
        .HasForeignKey<utblPrescription>(x => x.AppointmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(c => c.Doctor)
        .WithMany(p => p.Prescriptions)
        .HasForeignKey(x => x.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(c => c.Patient)
        .WithMany(p => p.Prescriptions)
        .HasForeignKey(x => x.PatientId)
        .OnDelete(DeleteBehavior.Restrict);


      builder.Property(x => x.IssuedOn)
        .HasDefaultValueSql("GETUTCNOW()");

      //builder.HasMany(c => c.Items) // using child centric approach
      //  .WithOne(p => p.Prescription)
      //  .HasForeignKey(x => x.PrescriptionId)
      //  .OnDelete(DeleteBehavior.Restrict);


    }
  }
}
