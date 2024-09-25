
using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.Common.Mappings;
using MyWebApi.Application.Common.Models;


namespace MyWebApi.Application.Rooms.Queries
{
   
    public record GetRoomsQuery : IRequest<PaginatedList<RoomDTO>>
    {
        public int? PageNumber { get; init; } = 1;
        public int? PageSize { get; init; } = 10;
    }
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, PaginatedList<RoomDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<RoomDTO>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms.AsNoTracking().ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).OrderBy(x => x.RoomNumber).PaginatedListAsync((int)request.PageNumber!, (int)request.PageSize!);
        }
    }
}