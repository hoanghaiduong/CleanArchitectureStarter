

using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Hotel> Hotels {get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
