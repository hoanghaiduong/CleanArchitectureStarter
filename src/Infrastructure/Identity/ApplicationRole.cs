using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Infrastructure.Identity;
public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; } 

}
