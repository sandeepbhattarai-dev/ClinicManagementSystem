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

      builder.Property(x => x.IssuedOn)
        .HasDefaultValueSql("GETUTCDATE()");

      builder.HasOne(c => c.Patient)
        .WithMany(p => p.Invoices)
        .HasForeignKey(c => c.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(c => c.Appointment)
        .WithOne(p => p.Invoice)
        .HasForeignKey<utblInvoice>(c => c.AppointmentId)
        .OnDelete(DeleteBehavior.Restrict);

      //builder.Property(c => c.TotalAmount)
      //  .HasComputedColumnSql("[dbo].[fn_GetInvoiceTotal]([Id])");


      builder.Property(c => c.TotalAmount)
        .HasPrecision(10, 2);

      //builder.HasQueryFilter(x => x.Appointment.I)
      builder.HasQueryFilter(x => x.Appointment!.Department!.IsActive);
    }
  }
}
