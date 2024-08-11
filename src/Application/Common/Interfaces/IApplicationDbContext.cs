using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<TodoList> TodoLists { get; }

    //DbSet<TodoItem> TodoItems { get; }
    DbSet<Hotel> Hotels { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
