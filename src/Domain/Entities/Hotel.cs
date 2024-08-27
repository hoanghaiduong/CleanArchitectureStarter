

using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Domain.Entities
{
    public class Hotel : BaseAuditEntityCustom
    {
        [Key]
        public string? HotelID { get; set; } = Guid.NewGuid().ToString(); // Auto-generate GUID

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address must be provided")]
        public string? Address { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone must be provided")]
        public string? Phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must be provided")]
        public string? Email { get; set; }

        public int? Stars { get; set; }

        public TimeOnly? CheckinTime { get; set; }

        public TimeOnly? CheckoutTime { get; set; }

        public virtual ICollection<Room> Rooms { get; private set; } = new List<Room>();

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; private set; } = new List<ApplicationUser>();

    }
}