using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.Common.Mappings;
using MyWebApi.Application.Common.Models;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.RoomTypes.Queries
{
    public record RoomTypesVM
    {
        public IReadOnlyCollection<RoomType>? RoomTypes { get; init; } = [];
    }
    public record GetRoomTypesQuery : IRequest<PaginatedList<RoomTypeDTO>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetRoomTypesHandler : IRequestHandler<GetRoomTypesQuery, PaginatedList<RoomTypeDTO>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomTypesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<RoomTypeDTO>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes
              .AsNoTracking().ProjectTo<RoomTypeDTO>(_mapper.ConfigurationProvider)
              .OrderBy(od => od.Name)
              .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
    }
}