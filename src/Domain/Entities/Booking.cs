namespace MyWebApi.Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? GuestId { get; set; }

    public int? RoomNumber { get; set; }

    public DateOnly? CheckinDate { get; set; }

    public DateOnly? CheckoutDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Guest? Guest { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room? RoomNumberNavigation { get; set; }
}
