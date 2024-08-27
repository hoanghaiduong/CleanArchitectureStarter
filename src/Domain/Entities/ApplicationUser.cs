using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }
    [PersonalData]
    public string? HotelID { get; set; }
    [JsonIgnore]
    public override string? PasswordHash { get; set; }

    public virtual IList<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual Hotel? Hotel { get; set; } = null!;
}
