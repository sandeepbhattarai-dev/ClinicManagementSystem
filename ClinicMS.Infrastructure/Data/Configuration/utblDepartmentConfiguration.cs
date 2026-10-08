using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblDepartmentConfiguration : IEntityTypeConfiguration<utblDepartment>
  {
    public void Configure(EntityTypeBuilder<utblDepartment> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Name)
        .IsRequired();

      builder.HasIndex(x => x.Name)
        .IsUnique();

      builder.Property(x => x.Name)
        .HasMaxLength(100);

      builder.ToTable(t => t.HasCheckConstraint(
        name: "CK_utblDepartment_Name_LongerThan3Char",
        sql: "LEN(TRIM([Name])) >= 3"
        ));

      builder.HasQueryFilter(x => x.IsActive);
    }
  }
}
