

using System.ComponentModel.DataAnnotations;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.Common.DTO
{
    public class HotelDTO
    {

        public string? Name { get; init; }


        public string? Address { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public int? Stars { get; init; }

        public TimeOnly? CheckinTime { get; init; }

        public TimeOnly? CheckoutTime { get; init; }

        public virtual ICollection<Room> Rooms { get; init; } = [];

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; init; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {

                CreateMap<Hotel, HotelDTO>();
                CreateMap<Room, HotelDTO>();
                CreateMap<ApplicationUser, HotelDTO>();
            }
        }
    }
}