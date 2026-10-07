using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblDoctorConfiguration : IEntityTypeConfiguration<utblDoctor>
  {
    public void Configure(EntityTypeBuilder<utblDoctor> builder)
    {
      builder.HasOne(x => x.ApplicationUser)
        .WithOne()
        .HasForeignKey(x => x.ApplicationUserId);

      throw new NotImplementedException();
    }
  }
}
