using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebApi.Domain.Constants;
using MyWebApi.Domain.Entities;

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

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
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
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedRolesAsync();
            await SeedDefaultUsersAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedRolesAsync()
    {
        var roles = new[]
        {
            new ApplicationRole { Name = Roles.Administrator, Description = "Quản lý toàn bộ hệ thống." },
            new ApplicationRole { Name = Roles.Staff, Description = "Nhân viên hệ thống." },
            new ApplicationRole { Name = Roles.Guest, Description = "Người dùng đăng ký như khách hàng." },
        };

        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role.Name!) == null)
            {
                var result = await _roleManager.CreateAsync(role);
                LogResult(result, $"Role '{role.Name}' created successfully.", $"Failed to create role '{role.Name}'");
            }
        }
    }

    private async Task SeedDefaultUsersAsync()
    {
        await CreateDefaultUserAsync("administrator@localhost", "Administrator1!", new[] { Roles.Administrator, Roles.Staff, Roles.Guest });
        await CreateDefaultUserAsync("staff@localhost", "staff@localhost", new[] { Roles.Staff });
        await CreateDefaultUserAsync("guest@localhost", "guest@localhost", new[] { Roles.Guest });
    }

    private async Task CreateDefaultUserAsync(string email, string password, string[] roles)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser { UserName = email, Email = email };
            var userResult = await _userManager.CreateAsync(user, password);
            LogResult(userResult, $"User '{email}' created successfully.", $"Failed to create user '{email}'");

            if (userResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, roles);
                LogResult(roleResult, $"User '{email}' added to roles successfully.", $"Failed to add user '{email}' to roles");
            }
        }
    }

    private void LogResult(IdentityResult result, string successMessage, string failureMessage)
    {
        if (result.Succeeded)
        {
            _logger.LogInformation(successMessage);
        }
        else
        {
            _logger.LogWarning($"{failureMessage}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
