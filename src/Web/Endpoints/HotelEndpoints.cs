

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Hotels.Command.CreateHotel;
using MyWebApi.Application.Hotels.Queries;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Web.Endpoints
{
    public class HotelEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(nameof(Hotel))
                .MapGet(GetHotels)
                .MapPost(CreateHotel);
        }

        [AllowAnonymous]
        private async Task<IResult> CreateHotel([FromServices] ISender sender, [FromBody] CreateHotelCommand command)
        {
            var hotel = await sender.Send(command);
            return Results.Ok(new
            {
                Message = "Created Hotel Successfully",
                Data = hotel
            });

        }

        [AllowAnonymous]
        private async Task<IResult> GetHotels([FromServices] ISender sender, [AsParameters] GetHotelsCommand command)
        {
            try
            {
                var results= await sender.Send(command);
                return Results.Ok(new {Message="Get Hotels Successfully", Data = results});
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new {StatusCode=400,ex.Message});
            }
        }

    }
}