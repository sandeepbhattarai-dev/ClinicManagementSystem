using ClinicMS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ClinicMS.Infrastructure.Data
{
  public class ApplicationDbContext : IdentityDbContext<utblApplicationUser, utblApplicationUserRoles, string>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<utblAppointment> utblAppointments { get; set; }
    public DbSet<utblDoctor> utblDoctors { get; set; }
    public DbSet<utblInvoice> utblInvoices { get; set; }
    public DbSet<utblInvoiceItem> utblInvoiceItems { get; set; }
    public DbSet<utblMedicalRecord> utblMedicalRecords { get; set; }
    public DbSet<utblPatient> utblPatients { get; set; }
    public DbSet<utblPrescription> utblPrescriptions { get; set; }
    public DbSet<utblPrescriptionItem> utblPrescriptionsItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.HasSequence<int>("PatientSeq")
        .StartsAt(1)
        .IncrementsBy(1);

      modelBuilder.HasSequence<int>("AppointmentSeq")
        .StartsAt(1)
        .IncrementsBy(1);

      modelBuilder.HasSequence<int>("InvoiceSeq")
        .StartsAt(1)
        .IncrementsBy(1);

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
  }
}
