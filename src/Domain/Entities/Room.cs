namespace MyWebApi.Domain.Entities;

public partial class Room
{
    public int RoomNumber { get; set; }

    public int? HotelId { get; set; }

    public int? TypeId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Hotel? Hotel { get; set; }

    public virtual RoomType? Type { get; set; }
}
