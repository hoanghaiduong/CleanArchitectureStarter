

using System.Text.Json.Serialization;
using MyWebApi.Domain.Entities;
using MyWebApi.Domain.Enums;


namespace MyWebApi.Application.Common.DTO
{
    public class RoomDTO
    {
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string? RoomID { get; set; }
        public int RoomNumber { get; set; }

        public string? HotelID { get; set; } // Assuming a room must belong to a hotel

        public string? RoomTypeID { get; set; }  // Each room must have a type

        public ERoom Status { get; set; } = ERoom.Available; // Assuming default status
        public virtual RoomType? RoomType { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Room, RoomDTO>();
                CreateMap<Hotel, RoomDTO>();
                CreateMap<RoomType, RoomDTO>();
                CreateMap<Booking, RoomDTO>();
            }
        }
    }
}