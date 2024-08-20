
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }
    [JsonIgnore]
    public override string? PasswordHash { get; set; }
}
