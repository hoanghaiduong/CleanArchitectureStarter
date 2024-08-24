using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Application.Common.Mappings;
using MyWebApi.Domain.Constants;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Infrastructure.Identity;
using MyWebApi.Web.Services;

namespace MyWebApi.Web.Endpoints
{
    public class IdentityUserEndPoint : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGet("/users", GetAllUsers);
            app.MapPost("sign-up", SignUpUser);

            app.MapPost("sign-in", SignInUserWithJWT);
        }

        [AllowAnonymous]
        private async Task<IResult> GetAllUsers(UserManager<ApplicationUser> _userManager, ApplicationDbContext _context)
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                var userRoles = await _context.UserRoles
                    .Join(_context.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => new { ur.UserId, RoleName = r.Name })
                    .ToListAsync();

                var usersWithRoles = users.Select(user => new
                {
                    User = user,
                    Roles = userRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Select(ur => ur.RoleName)
                        .ToList()
                });

                return Results.Ok(new
                {
                    Message = "Get All Users Successfully",
                    Data = usersWithRoles
                });
            }
            catch (System.Exception Ex)
            {
                return Results.BadRequest(Ex.Message);
            }
        }

        [AllowAnonymous]
        public async Task<IResult> SignInUserWithJWT(UserManager<ApplicationUser> _userManager, [FromBody] SignInModel dto, IOptions<AppSettings> appSettings)
        {
            // Generate new access token
            var accessTokenSecret = appSettings.Value.JWT_ACCESS_TOKEN_SECRET;
            Guard.Against.Null(accessTokenSecret, message: "JWT Access Token Secret 'JWT_ACCESS_TOKEN_SECRET' not found.");
            var refreshTokenSecret = appSettings.Value.JWT_REFRESH_TOKEN_SECRET;
            Guard.Against.Null(refreshTokenSecret, message: "JWT Refresh Token Secret 'JWT_REFRESH_TOKEN_SECRET' not found.");
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
                {

                    var roles = await _userManager.GetRolesAsync(user);

                    var accessToken = GenerateAccessToken(accessTokenSecret, user, roles);
                    var refreshToken = GenerateRefreshToken(refreshTokenSecret, user);
                    return Results.Ok(new { accessToken, refreshToken });
                }
                else
                {
                    Results.BadRequest(new
                    {
                        Message = "Sign In Failed Please Check Your Information"
                    });
                }
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        public async Task<IResult> SignUpUser(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, [FromBody] SignUpModel dto)
        {
            try
            {
                var newUser = new ApplicationUser
                {
                    Email = dto.Email,
                    UserName = dto.UserName ?? dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
                var result = await _userManager.CreateAsync(newUser, dto.Password);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, Roles.Guest);
                if (!addToRoleResult.Succeeded)
                {
                    return Results.BadRequest(addToRoleResult.Errors);
                }

                return Results.Ok(new
                {
                    Message = "Đăng Ký Người Dùng Thành Công !",
                    user = newUser
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        public async Task<IResult> RefreshToken(
        [FromBody] string refreshToken,
        UserManager<ApplicationUser> _userManager,
        IOptions<AppSettings> appSettings)
        {
            try
            {
                // Generate new access token
                var accessTokenSecret = appSettings.Value.JWT_ACCESS_TOKEN_SECRET;
                Guard.Against.Null(accessTokenSecret, message: "JWT Access Token Secret 'JWT_ACCESS_TOKEN_SECRET' not found.");
                var refreshTokenSecret = appSettings.Value.JWT_REFRESH_TOKEN_SECRET;
                Guard.Against.Null(refreshTokenSecret, message: "JWT Refresh Token Secret 'JWT_REFRESH_TOKEN_SECRET' not found.");
                // Validate the refresh token
                var principal = ValidateRefreshToken(refreshToken, refreshTokenSecret);
                if (principal == null)
                {
                    return Results.BadRequest(new { Message = "Invalid refresh token" });
                }

                // Get user ID from refresh token
                var userId = principal.FindFirst("UserID")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.BadRequest(new { Message = "Invalid refresh token" });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.NotFound(new { Message = "User not found" });
                }

                // Generate new tokens
                var roles = await _userManager.GetRolesAsync(user);
                var accessTokenString = GenerateAccessToken(accessTokenSecret, user, roles);
                var refreshTokenString = GenerateRefreshToken(refreshTokenSecret, user);

                // Return the new tokens
                return Results.Ok(new
                {
                    AccessToken = accessTokenString,
                    RefreshToken = refreshTokenString
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Message = ex.Message });
            }
        }

        private ClaimsPrincipal? ValidateRefreshToken(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false // Ignore token expiration for refresh token validation
                }, out _);

                return principal;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }

        private string GenerateRefreshToken(string refreshTokenSecret, ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("UserID", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var newRefreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
            return tokenHandler.WriteToken(newRefreshToken);
        }

        private string GenerateAccessToken(string accessTokenSecret, ApplicationUser user, IList<string> roles)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim("EmailVerified", user.EmailConfirmed.ToString()),
                new Claim("PhoneNumberConfirmed", user.PhoneNumberConfirmed.ToString()),
                // new Claim(ClaimTypes.Role, string.Join(",", roles))
            }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role))));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var newAccessToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(newAccessToken);
        }


        public class SignUpModel
        {
            [Required]
            public required string Email { get; set; }
            [Required]
            public required string Password { get; set; }

            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? UserName { get; set; }

        }
        public class SignInModel
        {
            [JsonIgnore]
            public string? UserName { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}