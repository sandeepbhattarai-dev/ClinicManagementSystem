using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Domain.Entities
{
  public class utblDoctor
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = default!;

		[Required]
    [MaxLength(50)]
    public string DepartmentId { get; set; } = default!;

		[Required]
    [MaxLength(100)]
    public string Specialization { get; set; } = default!;

		[Required]
    [MaxLength(30)]
    public string LicenseNumber { get; set; } = default!;

		public decimal ConsultationFee { get; set; }

    public bool IsActive { get; set; } = true;

    // navigation
    public utblApplicationUser? ApplicationUser { get; set; }
  }
}