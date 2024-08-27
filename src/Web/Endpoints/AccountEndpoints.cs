

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Web.Endpoints
{
    public class AccountEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup("account").MapGet(GetGetUserProfile);
            app.MapPost("addRoleToUserAccount", AddRoleToUserAccount);
        }
        [AllowAnonymous]
        private async Task<IResult> AddRoleToUserAccount(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, [FromBody] AddRoleToUserModel dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserID!);
                if (user == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "User does not exist"
                    });
                }
                var role = await _roleManager.FindByNameAsync(dto.RoleName!);
                if (role == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "Role does not exist"
                    });
                }
                //nếu user không chứa role chỉ định thì tạo role cho user còn lại thì thêm role mới cho user
                var roleExistInUser = await _userManager.IsInRoleAsync(user, role.Name!);
                if (!roleExistInUser)
                {
                    await _userManager.AddToRoleAsync(user, role.Name!);
                }
                return Results.Ok(new
                {
                    Message = "Add Role to User Successfully"
                });
            }
            catch (System.Exception Ex)
            {
                return Results.BadRequest(Ex.Message);
            }
        }

        [Authorize]
        private async Task<IResult> GetGetUserProfile(UserManager<ApplicationUser> _userManager, ClaimsPrincipal claims)
        {

            var userId = claims.Claims.First(x => x.Type == "UserID").Value;

            var user = await _userManager.FindByIdAsync(userId);
            return user switch
            {
                null => Results.NotFound(),
                _ => Results.Ok(new { user })
            };
        }
    }

    public class AddRoleToUserModel
    {
        [Required]
        public string? UserID { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
