
using System.ComponentModel.DataAnnotations;
namespace MyWebApi.Application.Common.DTO
{
    public class PaymentDTO
    {
 
        public string? BookingID { get; init; }

        public decimal? Amount { get; init; }

        public DateOnly? PaymentDate { get; init; }

        public string? PaymentMethod { get; init; }
    }
}