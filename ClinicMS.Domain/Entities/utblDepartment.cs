using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  internal class utblDepartment
  {
    [Key]
    [Required]
    [StringLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Department Name needs to be of minimum 3 characters and maximum 50 characters long.")]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;
  }
}
