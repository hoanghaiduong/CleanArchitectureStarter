
using System.ComponentModel.DataAnnotations;


namespace MyWebApi.Domain.Entities
{
    public class Payment :BaseAuditEntityCustom
    {
        [Key]//primary key
        public string PaymentID { get; set; } = Guid.NewGuid().ToString(); // Auto-generate GUID
        [Required]
        public string? BookingID { get; set; }

        public decimal? Amount { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public string? PaymentMethod { get; set; }

        public virtual Booking? Booking { get; set; }=null!;
    }
}