
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;


namespace MyWebApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestsController : ApiControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IApplicationDbContext _context;
    public TestsController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IApplicationDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }



    // [HttpPost("Register")]
    // [Authorize(Roles = Roles.Administrator, Policy = Policies.CanPurge)]
    // public async Task<ActionResult> RegisterUser([FromBody] CreateUserDTO dto)
    // {
    //     var newUser = new ApplicationUser { UserName = dto.Username, Email = dto.Email };

    //     var user = await _userManager.CreateAsync(newUser, dto.Password);
    //     var tokenCreate = await _userManager.CreateSecurityTokenAsync(newUser);
    //     return Ok(new
    //     {
    //         message = "This is a secret message",
    //         dto,
    //         tokenCreate,
    //         user
    //     });
    // }
    [HttpPost("SignIn")]
    public async Task<IResult> SignInUserWithJWT([FromBody] CreateUserCustomDTO dto)
    {
        var accessTokenSecret = _configuration.GetValue<string>("AppSettings:JWT_ACCESS_TOKEN_SECRET");
        Guard.Against.Null(accessTokenSecret, message: "Jwt Secret 'JWT_ACCESS_TOKEN_SECRET' not found.");
        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {

                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim("UserID",user.Id.ToString()),
                        ]),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandle = new JwtSecurityTokenHandler();
                var securityToken = tokenHandle.CreateToken(tokenDescriptor);
                var token = tokenHandle.WriteToken(securityToken);
                return Results.Ok(new { token });
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
    [HttpGet("test")]
    public async Task<IResult> TestAsync(){
        var result=await _context.Hotels.Include(x=>x.Rooms).ThenInclude(x=>x.RoomType).IgnoreAutoIncludes().Include(x=>x.ApplicationUsers).ToListAsync();
        return Results.Ok(result);
    }
    [HttpPost("create-custom")]
    public async Task<ActionResult> SignUpUser(CreateUserCustomDTO dto)
    {

        try
        {
            var newUser = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            var user = await _userManager.CreateAsync(newUser, dto.Password);
            return Ok(new
            {
                message = "Create Custom User With Identity Successfully",
                user
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
public class CreateUserCustomDTO
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
// public class CreateUserDTO
// {
//     [Required]
//     public string Username { get; set; } = string.Empty;

//     [Required]
//     public string Password { get; set; } = string.Empty;

//     public string? Email { get; set; }
// }


