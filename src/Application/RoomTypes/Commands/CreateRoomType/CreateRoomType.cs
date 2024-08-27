

using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.RoomTypes.Commands.CreateRoomType
{
    public record CreateRoomTypeCommand : IRequest<RoomType>
    {

        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal PricePerNight { get; init; }

        public int Capacity { get; init; }
    }
    public class CreateRoomTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateRoomTypeCommand,RoomType>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<RoomType> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new RoomType
            {

                Name = request.Name,
                Description = request.Description,
                PricePerNight = request.PricePerNight,
                Capacity = request.Capacity

            };
            _context.RoomTypes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
