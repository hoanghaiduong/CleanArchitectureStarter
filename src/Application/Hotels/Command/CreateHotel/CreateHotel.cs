
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Hotels.Command.CreateHotel
{
    public class CreateHotelCommand : IRequest<Hotel>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? Stars { get; set; }
        public TimeOnly? CheckinTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        public TimeOnly? CheckoutTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    }
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Hotel>
    {
        private readonly IApplicationDbContext _context;

        public CreateHotelCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var entity = new Hotel
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Stars = request.Stars,
                CheckinTime = request.CheckinTime,
                CheckoutTime = request.CheckoutTime,
            };
            _context.Hotels.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}