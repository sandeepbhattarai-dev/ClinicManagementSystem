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
    public string InvoiceNumber { get; set; } = default!;
    public string PatientId { get; set; } = default!;
    public string AppointmentId { get; set; } = default!;
    public DateTime IssuedOn { get; set; } = DateTime.UtcNow;
    public InvoiceStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime PaidOn { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }

  public class utblInvoiceItem
  {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string InvoiceId { get; set; } = default!;
    public string Description { get; set; }
    public string Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal { get; set; }
  }
}
