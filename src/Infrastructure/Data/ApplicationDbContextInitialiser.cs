using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebApi.Domain.Constants;
using MyWebApi.Infrastructure.Identity;


namespace MyWebApi.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var roles = new[]{
            new ApplicationRole{
                Name = Roles.Administrator,
                Description = "Quản lý toàn bộ hệ thống, bao gồm quản lý admin, người dùng, phòng, dịch vụ, v.v.",
            },
            new ApplicationRole{
                Name = Roles.Admin,
                Description = "Quản lý hệ thống, người dùng, phòng, dịch vụ.",
            },
            new ApplicationRole{
                Name = Roles.Manager,
                Description = "Quản lý khách sạn cụ thể, bao gồm quản lý đặt phòng, khách hàng, và dịch vụ tại khách sạn đó.",
            },
            new ApplicationRole{
                Name = Roles.Receptionist,
                Description = "Xử lý check-in, check-out, và hỗ trợ khách hàng tại khách sạn.",
            },
               new ApplicationRole{
                Name = Roles.Guest,
                Description = "Người dùng đăng ký như khách hàng, chỉ có quyền truy cập vào thông tin của chính mình.",
            },
        };


        // Check and create roles if not exist
        foreach (var role in roles)
        {

            if (await _roleManager.FindByNameAsync(role.Name!) == null)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Role '{role.Name}' created successfully.");
                }
                else
                {
                    _logger.LogWarning($"Failed to create role '{role.Name}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

        }


        // Default admin user
        var adminEmail = "administrator@localhost";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
            var userResult = await _userManager.CreateAsync(adminUser, "Administrator1!");

            if (userResult.Succeeded)
            {
                _logger.LogInformation($"User '{adminUser.UserName}' created successfully.");

                // Add to Administrator role
                var addToRoleResult = await _userManager.AddToRolesAsync(adminUser, roles.Select(r=>r.Name!));
                if (addToRoleResult.Succeeded)
                {
                    _logger.LogInformation($"User '{adminUser.UserName}' added to roles successfully.");
                }
                else
                {
                    _logger.LogWarning($"Failed to add user '{adminUser.UserName}' to roles: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                _logger.LogWarning($"Failed to create user '{adminUser.UserName}': {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}
