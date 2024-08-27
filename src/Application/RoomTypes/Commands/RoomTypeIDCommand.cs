
namespace MyWebApi.Application.RoomTypes.Commands
{
    public record RoomTypeIDCommand : IRequest
    {
         public string? RoomTypeID { get; init; }
    }
}