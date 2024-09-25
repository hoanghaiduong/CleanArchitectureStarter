


using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.DTO;
public class RoomTypeDTO
{

    public string? RoomTypeID { get; set; }
    public string? Name { get; init; }
    public string? Description { get; init; }

    public decimal PricePerNight { get; init; }

    public int Capacity { get; init; }
    public virtual ICollection<Room> Rooms { get; init; } = [];
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoomType, RoomTypeDTO>();
        }
    }

}
