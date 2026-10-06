using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblPatientConfiguration : IEntityTypeConfiguration<utblPatient>
  {
    public void Configure(EntityTypeBuilder<utblPatient> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasIndex(x => x.PatientNumber)
        .IsUnique();

      builder.Property(x => x.PatientNumber)
        .HasDefaultValueSql("('PAT-' + RIGHT('000000' + CAST(NEXT VALUE FOR PatientSeq AS VARCHAR(6)), 6))");

      builder.ToTable(t => t.HasCheckConstraint(
        "Patient_DateOfBirth_NoFuture", "[DateOfBirth] <= GetDate()"
        ));

      builder.HasQueryFilter(x => x.IsActive);

      builder.Property(x => x.CreatedOn)
        .HasDefaultValueSql("GETUTCDATE()");
    }
  }
}
