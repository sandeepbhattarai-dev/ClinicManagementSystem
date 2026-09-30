using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  internal class utblDoctorSchedule
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string DoctorId { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public utblDoctor Doctor { get; set; } = default!;

    public DayOfWeek DayOfWeek { get; set; }

    [Required]
    public DateOnly DoctorVisitDate { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan Endime { get; set; }

    public int SlotDurationMinutes { get; set; } = 30;
    public bool IsActive { get; set; } = true;
  }
}
