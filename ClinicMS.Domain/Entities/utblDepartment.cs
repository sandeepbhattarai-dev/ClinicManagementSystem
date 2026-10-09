using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  public class utblDepartment
  {
    [Key]
    [Required]
    [StringLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    //public bool IsDeleted { get; set; } = false;


    // navigation
    public List<utblDoctor> Doctors { get; set; } = [];
    //public List<utblDepartment> Departments { get; set; } = [];
    public List<utblAppointment> Appointments { get; set; } = [];
  }
}
