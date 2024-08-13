using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


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
    private readonly THUCHANHNET8_DAFContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, THUCHANHNET8_DAFContext context)
    {
        _logger = logger;
        _context = context;
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
        if (!_context.Hotels.Any())
        {
            // Get the next available ID
            var maxId = 0;
            var nextId = maxId + 1;

            var hotels = new[]
           {
                new Hotel
                {
                    HotelID=nextId++,
                    Name = "Hoàng Gia Hotel",
                    Address = "Hoàng Bá Bích"
                },
                new Hotel
                {
                     HotelID=nextId++,
                    Name = "Hoàng Gia Hotel 1",
                    Address = "Hoàng Bá Bích 1"
                }
            };

            await _context.Hotels.AddRangeAsync(hotels);
            await _context.SaveChangesAsync();
        }
    }
}
