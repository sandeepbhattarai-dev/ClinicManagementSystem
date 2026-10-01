using System.ComponentModel.DataAnnotations;
using static ClinicMS.Domain.Enums.ClinicMSEnums;

namespace ClinicMS.Domain.Entities
{
  public class utblInvoice
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(30)]
    public string InvoiceNumber { get; set; } = default!;
    [Required]
    [MaxLength(50)]
    public string PatientId { get; set; } = default!;
    [Required]
    [MaxLength(50)]
    public string AppointmentId { get; set; } = default!;

    public DateTime IssuedOn { get; set; } = DateTime.UtcNow;
    public InvoiceStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime PaidOn { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }

  public class utblInvoiceItem
  {
    [Key]
    [Required]
    [MaxLength(50)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(50)]
    public string InvoiceId { get; set; } = default!;
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public string Quantity { get; set; } = default!;

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(1, double.MaxValue)]
    public decimal LineTotal { get; set; }
  }
}
