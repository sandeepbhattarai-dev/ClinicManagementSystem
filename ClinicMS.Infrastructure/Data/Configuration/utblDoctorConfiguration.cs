using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblDoctorConfiguration : IEntityTypeConfiguration<utblDoctor>
  {
    public void Configure(EntityTypeBuilder<utblDoctor> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasOne(x => x.ApplicationUser) // nav in child
        .WithOne() // nav in parent but not present here
        .HasForeignKey<utblDoctor>(x => x.ApplicationUserId) // child, foreign key
        .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(c => c.Department)
        .WithMany(p => p.Doctors)
        .HasForeignKey(f => f.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Property(x => x.FullName)
        .IsRequired();

      builder.Property(x => x.Specialization)
        .IsRequired();

      builder.Property(x => x.LicenseNumber)
        .IsRequired(true);

      builder.HasIndex(x => x.LicenseNumber)
        .IsUnique();

      builder.ToTable(t => t.HasCheckConstraint(
        name: "CK_UtblDoctor_LicenseNumber_ShouldNotBeNull",
        sql: "LEN(TRIM([LicenseNumber])) > 0"
        ));


      builder.ToTable(x => x.HasCheckConstraint(
        name: "CK_UtblDoctor_ConsultationFee_GreaterThanZero",
        sql: "[ConsultationFee] >= 0"));

      builder.HasQueryFilter(x => x.IsActive);
    }
  }
}
