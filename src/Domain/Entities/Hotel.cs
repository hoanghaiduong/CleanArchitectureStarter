using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Domain.Entities
{
    //Kết hợp annotation
    public class Hotel : BaseAuditableEntity
    {
        public int HotelID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty!;

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Range(1, 5)]
        public string? Stars { get; set; }

        public DateTime? CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
    }
}
