using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  public class utblPrescription
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string AppointmentId { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string PatientId { get; set; } = default!;

    [MaxLength(50)]
    public string? DoctorId { get; set; }

    public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Notes { get; set; }


    public ICollection<utblPrescriptionItem> Items { get; set; } = [];

    public utblAppointment? Appointment { get; set; }
    public utblDoctor? Doctor { get; set; }
    public utblPatient? Patient { get; set; }
  }


  public class utblPrescriptionItem
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string PrescriptionId { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string MedicationName { get; set; } = default!;


    [MaxLength(100)]
    public string Dosage { get; set; } = default!;


    [MaxLength(100)]
    public string Frequency { get; set; } = default!;

    [Range(1, 90)]
    public int DurationDays { get; set; }

    [MaxLength(500)]
    public string? Instructions { get; set; }


    //navigation property
    public utblPrescription? Prescription { get; set; }
  }
}
