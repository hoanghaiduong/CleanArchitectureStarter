

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Domain.Entities
{
    public class Booking : BaseAuditEntityCustom
    {
        [Key]
        public string BookingID { get; set; } =Guid.NewGuid().ToString(); // Auto-generate GUID

        // Foreign key to the Guest (User)
        [Required]
        [Column("UserID")]
        public string? GuestID { get; set; }
        // Foreign key to the Room
        [Required]
        public string? RoomID { get; set; } 

        [Required]
        public DateOnly CheckinDate { get; set; }

        [Required]
        public DateOnly CheckoutDate { get; set; }

        public decimal TotalPrice { get; set; }

        // Navigation properties
        public virtual Room? Room { get; set; }=null!;
     
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}