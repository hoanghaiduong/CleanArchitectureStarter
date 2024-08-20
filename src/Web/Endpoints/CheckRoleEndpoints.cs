
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MyWebApi.Domain.Constants;

namespace MyWebApi.Web.Endpoints
{
    public class CheckRoleEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
           
            app.MapGet("administrator-manager-guest-can-access", [Authorize(Roles = $"{Roles.Administrator},{Roles.Manager},{Roles.Guest}")] (ClaimsPrincipal user) =>
            {
                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return $"User Roles: {string.Join(", ", roles)}";
            });
            app.MapGet("administrator-manager-can-access", [Authorize(Roles = $"{Roles.Administrator},{Roles.Manager}")] (ClaimsPrincipal user) =>
            {
                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return $"User Roles: {string.Join(", ", roles)}";
            });
            app.MapGet("administrator-admin-can-access", [Authorize(Roles = $"{Roles.Administrator},{Roles.Manager}")] (ClaimsPrincipal user) =>
            {
                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return $"User Roles: {string.Join(", ", roles)}";
            });
            app.MapGet("admin-guest-can-access", [Authorize(Roles = $"{Roles.Administrator},{Roles.Guest}")] (ClaimsPrincipal user) =>
            {
                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return $"User Roles: {string.Join(", ", roles)}";
            });
            app.MapGet("Administrator-only", [Authorize(Roles = Roles.Administrator)] () =>
            {
                return "Administrator Only";
            });
            app.MapGet("Manager-only", [Authorize(Roles = Roles.Manager)] () =>
            {
                return "Manager Only";
            });
            app.MapGet("guest-only", [Authorize(Roles = Roles.Guest)] () =>
            {
                return "Guest Only";
            });
            app.MapGet("receptionist-only", [Authorize(Roles = Roles.Receptionist)] () =>
            {
                return "Receptionist Only";
            });
            
            app.MapGet("Admin-only", [Authorize(Roles = Roles.Admin)] () =>
            {
                return "Admin Only";
            });

        }


    }
}