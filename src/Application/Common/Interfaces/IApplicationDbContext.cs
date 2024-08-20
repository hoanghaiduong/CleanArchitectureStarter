


namespace MyWebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<Hotel> Hotels { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
