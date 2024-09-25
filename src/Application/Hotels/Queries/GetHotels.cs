

using MyWebApi.Application.Common.DTO;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.Common.Mappings;
using MyWebApi.Application.Common.Models;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Hotels.Queries
{
    public class GetHotelsCommand : IRequest<PaginatedList<HotelDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetHotelsCommandHandler : IRequestHandler<GetHotelsCommand, PaginatedList<HotelDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHotelsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HotelDTO>> Handle(GetHotelsCommand request, CancellationToken cancellationToken)
        {
            var entities = await _context.Hotels.Include(x=>x.ApplicationUsers).Include(x=>x.Rooms).AsNoTracking().ProjectTo<HotelDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize);
            return entities;
        }
    }
}