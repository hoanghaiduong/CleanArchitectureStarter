

using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommand : IRequest<string>
    {

        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal PricePerNight { get; init; }

        public int Capacity { get; init; }
    }
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomTypeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
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
            return entity.RoomTypeID;
        }
    }
}
