
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.RoomTypes.Queries
{
    public class RoomTypeDTO
    {
        public RoomTypeDTO()
        {
            Rooms = [];
        }

        public string? RoomTypeID { get; init; }
        public string? Name { get; init; }

        public string? Description { get; init; }
       
        public decimal PricePerNight { get; init; }
       
        public int Capacity { get; init; }

        public  IReadOnlyCollection<Room> Rooms { get; init; } 
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<RoomType, RoomTypeDTO>();
            }
        }

    }

}