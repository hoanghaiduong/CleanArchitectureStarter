

using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Booking> Bookings { get; }

    public DbSet<Guest> Guests { get; }

    public DbSet<Hotel> Hotels { get; }

    public DbSet<Payment> Payments { get; }

    public DbSet<Room> Rooms { get; }

    public DbSet<RoomType> RoomTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
