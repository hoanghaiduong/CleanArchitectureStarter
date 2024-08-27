


using Ardalis.GuardClauses;
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Application.RoomTypes.Commands.DeleteRoomType
{
    public record DeleteRoomTypeCommand : IRequest
    {
        public string? RoomTypeID { get; init; }
    }
    public class DeleteRoomTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteRoomTypeCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
          var entity = await _context.RoomTypes
             .Where(l => l.RoomTypeID == request.RoomTypeID)
             .SingleOrDefaultAsync(cancellationToken);
            Guard.Against.NotFound(request.RoomTypeID!, entity);
            _context.RoomTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}