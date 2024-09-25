
using MyWebApi.Domain.Entities;
using MyWebApi.Domain.Enums;

namespace MyWebApi.Application.Rooms.DTO
{
    public  class RoomBaseDTO
    {
        public int RoomNumber { get; init; }

        public string? HotelID { get; init; }

        public string? RoomTypeID { get; init; }

        public ERoom Status { get; init; } = ERoom.Available;

        private class Mapping:Profile{
            public Mapping(){
                CreateMap<Room,RoomBaseDTO>();
            }
        }
    }
}