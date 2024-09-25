using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWebApi.Domain.Entities;


namespace MyWebApi.Application.Common.DTO
{
    public class BookingDTO
    {
        public string? GuestID { get; init; }

        public string? RoomID { get; init; } 

        public DateOnly CheckinDate { get; init; }

        public DateOnly CheckoutDate { get; init; }

        public decimal TotalPrice { get; init; }
    }
}