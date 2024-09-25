
using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Application.Rooms.Queries
{
    public record GetRoomQuery(string RoomID) : IRequest<RoomDTO>;
    public record GetRoomQueryCommandHandler : IRequestHandler<GetRoomQuery, RoomDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomQueryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDTO> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.AsNoTracking().Include(x => x.RoomType).Include(x => x.Hotel).Include(x => x.Bookings).ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.RoomID == request.RoomID,cancellationToken);
            return entity!;
        }
    }
}