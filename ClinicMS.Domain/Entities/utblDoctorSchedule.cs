using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  public class utblDoctorSchedule
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string DoctorId { get; set; } = default!;

    public DayOfWeek DayOfWeek { get; set; }

    //[Required]
    //public DateOnly DoctorVisitDate { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public int SlotDurationMinutes { get; set; } = 30;
    public bool IsActive { get; set; } = true;

    // navigation 
    public utblDoctor? Doctor { get; set; }
  }
}
