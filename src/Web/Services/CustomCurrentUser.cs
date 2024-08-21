

using System.Security.Claims;

namespace MyWebApi.Web.Services
{
    public class CustomCurrentUser
    {
        public ClaimsPrincipal? _claimsPrincipal { get; set;}
        public string? UserID => _claimsPrincipal?.Claims?.First(x => x.Type == "UserID")?.Value;
    }

}