namespace MyWebApi.Domain.Entities;
public partial class Hotel
{
    public int HotelId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? Stars { get; set; }

    public TimeOnly? CheckinTime { get; set; }

    public TimeOnly? CheckoutTime { get; set; }

    //public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
