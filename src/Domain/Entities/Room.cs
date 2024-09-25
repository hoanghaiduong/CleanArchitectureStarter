
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MyWebApi.Domain.Enums;

namespace MyWebApi.Domain.Entities
{
    public class Room : BaseAuditEntityCustom
    {
        [Key]
        public string RoomID { get; set; } = Guid.NewGuid().ToString(); // Auto-generate GUID
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public  string? HotelID { get; set; } // Assuming a room must belong to a hotel
        [Required]
        public  string? RoomTypeID { get; set; }  // Each room must have a type

        public ERoom Status { get; set; } = ERoom.Available; // Assuming default status

       
        // Navigation properties
        public virtual RoomType? RoomType { get; set; } = null!;
        public virtual Hotel? Hotel { get; set; } =null!;
        public virtual ICollection<Booking> Bookings { get; set; } = [];
        
      
    }
}