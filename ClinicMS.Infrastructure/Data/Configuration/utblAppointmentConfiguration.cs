using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblAppointmentConfiguration : IEntityTypeConfiguration<utblAppointment>
  {
    public void Configure(EntityTypeBuilder<utblAppointment> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.AppointmentNumber)
        .IsRequired()
        .HasDefaultValueSql("('APT-' + RIGHT('000000' + CAST(NEXT VALUE FOR AppointmentSeq AS VARCHAR(6)), 6))");

      builder.HasOne(c => c.Patient)
        .WithMany(p => p.Appointments)
        .HasForeignKey(c => c.PatientId)
        .OnDelete(DeleteBehavior.Restrict);


      builder.HasOne(c => c.Doctor)
        .WithMany(p => p.Appointments)
        .HasForeignKey(c => c.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(c => c.Department)
        .WithMany(p => p.Appointments)
        .HasForeignKey(c => c.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.ToTable(t =>
      {
        t.HasCheckConstraint(
          name: "CK_utblAppointment_AppointmentDate_AppointmentDateInFuture",
          sql: "[AppointmentDate] >= CAST(GETUTCDATE() AS DATE)"
          );

        t.HasCheckConstraint(
          name: "CK_utblAppointment_EndTime_EndTimeShouldBeLaterThanStartTime",
          sql : "[EndTime] > [StartTime]"
);

      });

      builder.Property(x => x.ChiefComplaint)
        .IsRequired();

      builder.Property(x => x.CreatedOn)
        .HasDefaultValueSql("GETUTCDATE()");

      builder.Property(x => x.CreatedById)
        .IsRequired();
    }
  }
}
