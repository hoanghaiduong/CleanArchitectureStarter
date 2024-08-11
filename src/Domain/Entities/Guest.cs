namespace MyWebApi.Domain.Entities;

public partial class Guest
{
    public int GuestId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
