using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<Guest> Guests => Set<Guest>();

    public DbSet<Hotel> Hotels => Set<Hotel>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
