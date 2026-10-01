using ClinicMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicMS.Infrastructure.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext()
    {

    }

    public DbSet<utblAppointment> utblAppointments { get; set; }
    public DbSet<utblDoctor> utblDoctors { get; set; }
    public DbSet<utblInvoice> utblInvoices { get; set; }
    public DbSet<utblInvoiceItem> utblInvoiceItems { get; set; }
    public DbSet<utblMedicalRecord> utblMedicalRecords { get; set; }
    public DbSet<utblPatient> utblPatients { get; set; }
    public DbSet<utblPrescription> utblPrescriptions { get; set; }
    public DbSet<utblPrescriptionItem> utblPrescriptionsItem { get; set; }
  }
}
