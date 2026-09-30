using System.ComponentModel.DataAnnotations;
using static ClinicMS.Domain.Enums.ClinicMSEnums;

namespace ClinicMS.Domain.Entities
{
  public class utblPatient
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(30)]
    public string PatientNumber { get; set; } = default!;


		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; } = default!;

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; } = default!;

    public DateTime DateOfBirth { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public BloodGroup BloodGroup { get; set; }


    [Required]
    [MaxLength(15)]
    public string Phone { get; set; } = default!;

    public string? Email { get; set; }
    public string? Address { get; set; }


    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(50)]
    public string CreatedById { get; set; } = default!;
  }
}
