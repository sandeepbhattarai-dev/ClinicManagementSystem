using System.ComponentModel.DataAnnotations;
using static ClinicMS.Domain.Enums.ClinicMSEnums;

namespace ClinicMS.Domain.Entities
{
  public class utblAppointment
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string AppointmentNumber { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string PatientId { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string DoctorId { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string DepartmentId { get; set; } = default!;

    [Required]
    public DateOnly AppointmentDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }
    [Required]
    public TimeSpan EndTime { get; set; }
    [Required]
    public AppointmentStatus Status { get; set; }

    public string ChiefComplaint { get; set; } = default!;

    [MaxLength(50)]
    [MinLength(10)]
    public string? CancelReason { get; private set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    [Required]
    [MaxLength(50)]
    public string CreatedById { get; set; } = default!;

    public void CancelAppointment(string reason, string updatedByUserId)
    {
      if (string.IsNullOrWhiteSpace(reason) || reason.Trim().Length < 10)
      {
        throw new ArgumentException("A reason must be provided for cancelling. The reason must be atleast be 10 characters long.");
      }
      else
      {
        Status = AppointmentStatus.Cancelled;
        CancelReason = reason;
      }
    }

    public utblPatient? Patient { get; set; }
    public utblDoctor? Doctor { get; set; }
    public utblDepartment? Department { get; set; }

    public utblMedicalRecord? MedicalRecord { get; set; }
    public utblPrescription? Prescription { get; set; }


  }
}
