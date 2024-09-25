using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;


namespace MyWebApi.Application.RoomTypes.Queries
{

    public record GetRoomTypeQuery(string RoomTypeID) : IRequest<RoomTypeDTO>;
    public class GetRoomQueryHandler : IRequestHandler<GetRoomTypeQuery, RoomTypeDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetRoomQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomTypeDTO> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.RoomTypes
            .AsNoTracking()
            .Include(x => x.Rooms)
            .ProjectTo<RoomTypeDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.RoomTypeID == request.RoomTypeID, cancellationToken);

            return entity!;
        }
    }
}