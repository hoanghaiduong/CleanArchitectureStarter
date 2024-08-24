
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Domain.Entities
{
    public class RoomType : BaseAuditEntityCustom
    {
        [Key]
        public string RoomTypeID { get; set; } = Guid.NewGuid().ToString(); // Auto-generate GUID
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [Required]
        public decimal PricePerNight { get; set; }
        [Required]
        public int Capacity { get; set; }

        // Navigation properties
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}