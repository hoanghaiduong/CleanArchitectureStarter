using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Domain.Entities;
public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }

}
