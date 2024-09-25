
using Ardalis.GuardClauses;
using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.Rooms.DTO;
#nullable disable

namespace MyWebApi.Application.Rooms.Command.UpdateRoom
{
    public class UpdateRoomCommandWithID : IRequest<RoomDTO>
    {
        public string RoomID { get; init; } = null!;
        public UpdateRoomCommand Command { get; init; } = new UpdateRoomCommand();
    }
    public class UpdateRoomCommand : RoomBaseDTO, IRequest<RoomDTO>;
    public class UpdateRoomCommanHandler : IRequestHandler<UpdateRoomCommandWithID, RoomDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateRoomCommanHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDTO> Handle(UpdateRoomCommandWithID request, CancellationToken cancellationToken)
        {


            var room = await _context.Rooms.FindAsync([request.RoomID], cancellationToken);
            // Handle not found case
            // Guard.Against.NotFound(request.RoomID, room);
            if (room == null)
            {
                return null;
            }
            else
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.HotelID == request.Command.HotelID, cancellationToken);
                if (hotel == null)
                {
                    return null;
                }
                var roomType = await _context.RoomTypes.FirstOrDefaultAsync(x => x.RoomTypeID == request.Command.RoomTypeID, cancellationToken);
                if (roomType == null)
                {
                    return null;
                }
                // Guard.Against.NotFound(request.Command.HotelID!, request.Command.HotelID);
                // Guard.Against.NotFound(request.Command.RoomTypeID!, request.Command.RoomTypeID);

                room.RoomNumber = request.Command.RoomNumber;
                room.HotelID = request.Command.HotelID;
                room.RoomTypeID = request.Command.RoomTypeID;
                room.Status = request.Command.Status;
                room.RoomType = roomType;
                room.Hotel = hotel;
                await _context.SaveChangesAsync(cancellationToken);
                var result = await _context.Rooms
                    .Include(x => x.Hotel)
                    .Include(x => x.RoomType)
                    .Include(x => x.Bookings)
                    .AsNoTracking().ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.RoomID == request.RoomID, cancellationToken);
                return result!;

            }


        }
    }

}