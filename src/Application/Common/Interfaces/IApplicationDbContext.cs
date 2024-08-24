


using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<RoomType> RoomTypes { get; }
    
    DbSet<Room> Rooms { get; }
    DbSet<Hotel> Hotels { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<Payment> Payments { get; }
   
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
