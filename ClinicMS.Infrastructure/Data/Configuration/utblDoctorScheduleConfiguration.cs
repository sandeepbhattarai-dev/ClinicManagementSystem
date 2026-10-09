using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblDoctorScheduleConfiguration : IEntityTypeConfiguration<utblDoctorSchedule>
  {
    public void Configure(EntityTypeBuilder<utblDoctorSchedule> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasOne(c => c.Doctor)
        .WithMany(p => p.DoctorSchedules)
        .HasForeignKey(x => x.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

      //builder.Property(x => x.DoctorVisitDate)
      //  .IsRequired();

      builder.ToTable(t =>
      {
        t.HasCheckConstraint(
          name: "CK_utblDoctorSchedule_EndTime_EndTimeShouldBeAfterStartTime",
          sql: "[EndTime] > [StartTime]"
          );
      });

      builder.Property(x => x.SlotDurationMinutes)
        .HasDefaultValue(30);

      builder.Property(x => x.IsActive)
        .HasDefaultValue(true);

      //builder.HasQueryFilter(x => x.IsActive);
      builder.HasQueryFilter(s => s.IsActive && s.Doctor!.IsActive);

    }
  }
}
