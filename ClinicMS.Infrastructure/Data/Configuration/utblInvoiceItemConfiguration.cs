using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMS.Infrastructure.Data.Configuration
{
  public class utblInvoiceItemConfiguration : IEntityTypeConfiguration<utblInvoiceItem>
  {
    public void Configure(EntityTypeBuilder<utblInvoiceItem> builder)
    {
      builder.HasKey(x => x.Id);

      builder.HasOne(c => c.Invoice)
        .WithMany(p => p.InvoiceItems)
        .HasForeignKey(f => f.InvoiceId)
        .OnDelete(DeleteBehavior.Cascade);
      builder.Property(x => x.Description)
        .IsRequired();

      builder.ToTable(t =>
      {
        t.HasCheckConstraint(
          name: "CK_utblInvoiceItem_Quantity_GreaterThanOrEqualTo1",
          sql: "[Quantity] >= 1"
          );

        t.HasCheckConstraint(
          name: "CK_utblInvoiceItem_UnitPrice_GreaterThanOrEqualTo0",
          sql: "[UnitPrice] >= 0"
          );


      });

      builder.Property(x => x.LineTotal)
        .HasComputedColumnSql("[Quantity] * [UnitPrice]");

      builder.Property(x => x.LineTotal)
        .HasPrecision(10,2);

      builder.Property(x => x.UnitPrice)
        .HasPrecision(10,2);

      builder.HasQueryFilter(s => s.Invoice!.Appointment!.Department!.IsActive);


    }
  }
}
