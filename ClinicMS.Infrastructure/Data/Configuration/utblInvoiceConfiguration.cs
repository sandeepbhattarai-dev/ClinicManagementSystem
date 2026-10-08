using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblInvoiceConfiguration : IEntityTypeConfiguration<utblInvoice>
  {
    public void Configure(EntityTypeBuilder<utblInvoice> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.InvoiceNumber)
        .HasDefaultValueSql("('Inv-' + Right('000000' + CAST(Next Value For InvoiceSeq AS VARCHAR(6)), 6))");
      builder.HasIndex(x => x.InvoiceNumber)
        .IsUnique();

      builder.
      throw new NotImplementedException();
    }
  }
}
