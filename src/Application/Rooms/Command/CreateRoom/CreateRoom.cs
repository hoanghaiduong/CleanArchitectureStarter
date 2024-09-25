using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.Rooms.DTO;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Rooms.Command.CreateRoom
{

    public class CreateRoomCommand : RoomBaseDTO, IRequest<RoomDTO>
    {

    }
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDTO> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.AsNoTracking().ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.HotelID == request.HotelID, cancellationToken);
            var roomType = await _context.RoomTypes.AsNoTracking().ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.RoomTypeID == request.RoomTypeID, cancellationToken);
            var entity = new Room
            {
                RoomNumber = request.RoomNumber,
                HotelID = request.HotelID!,
                RoomTypeID = request.RoomTypeID!,
                Status = request.Status,
                Hotel = hotel?.Hotel,
                RoomType = roomType?.RoomType
            };
            _context.Rooms.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            var room = await _context.Rooms.AsNoTracking().Include(x => x.Hotel).Include(x => x.RoomType).Include(x => x.Bookings).ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.RoomID == entity.RoomID, cancellationToken: cancellationToken);
            return new RoomDTO
            {
                RoomNumber = request.RoomNumber,
                RoomType = room?.RoomType,
                Hotel = room?.Hotel,
            };
        }


    }
}