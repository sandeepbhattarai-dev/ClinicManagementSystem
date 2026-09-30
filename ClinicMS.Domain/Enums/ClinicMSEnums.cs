namespace ClinicMS.Domain.Enums
{
  public class ClinicMSEnums
  {
    public enum Gender  { Male, Female, Other }
    
    public enum BloodGroup  { APositive, ANegative, BPositive, BNegative, OPositive, ONegative, ABPositive, ABNegative, Unknown }
    
    public enum AppointmentStatus { Scheduled, CheckedIn, Completed, Cancelled, NoShow }
    
    public enum InvoiceStatus { Pending, Paid, Cancelled }
    
    public enum PaymentMethod { Cash, Card, Insurance, Online }
  }
}
