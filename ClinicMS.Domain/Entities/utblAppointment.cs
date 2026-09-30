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
    [MaxLength (50)]
    public string DepartmentId { get; set; } = default!;
    [Required]
    public DateOnly AppointmentDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }
    [Required]
    public TimeSpan EndTime { get; set; }
    [Required]
    public AppointmentStatus Status { get; set; }

    public string? ChiefComplaint { get; set; }

    [MaxLength(50)]
    [MinLength(10)]
    public string? CancelReason { get; private set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    [Required]
    public string? CreatedById { get; set; }

    public void CancelAppointment(string reason, string updatedByUserId)
    {
      if (string.IsNullOrWhiteSpace(reason) && reason.Length >= 10)
      {
        throw new ArgumentException("A reason must be provided for cancelling. Reason needs to be of atleast 10 characters.");
      }
      Status = AppointmentStatus.Cancelled;
      CancelReason = reason;
    }
  }
}
