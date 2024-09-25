

using MyWebApi.Application.Common.DTO;

namespace MyWebApi.Application.Hotels.Command.UpdateHotel
{
    public record UpdateHotelCommandWithID : IRequest<HotelDTO>
    {
        public string? HotelID { get; init; }
        public UpdateHotelCommand? Command { get; set; }

    }
    public class UpdateHotelCommand : HotelDTO, IRequest<HotelDTO>;
}