
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Identity;
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
