using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  public class utblMedicalRecord
  {
    // primary key
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();


    [Required]
    [MaxLength(50)]
    public string AppointmentId { get; set; } = default!;

    [MaxLength(30)]
    public string? BloodPressure { get; set; }
    public decimal Temperature { get; set; }
    public decimal Pulse { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }

    [Required]
    [MaxLength(50)]
    public string Diagnosis { get; set; } = default!;

    [MaxLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(50)]
    public string CreatedById { get; set; } = default!;
    
    //navigation property
    public utblAppointment? Appointment { get; set; }
    public utblDoctor? Doctor { get; set; }
  }
}
