using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Common.Models;
using MyWebApi.Application.RoomTypes.Commands.CreateRoomType;
using MyWebApi.Application.RoomTypes.Commands.DeleteRoomType;
using MyWebApi.Application.RoomTypes.Commands.UpdateRoomType;
using MyWebApi.Application.RoomTypes.Queries;

namespace MyWebApi.Web.Endpoints
{
    public class RoomTypeEndpoints : EndpointGroupBase
    {

        public override void Map(WebApplication app)
        {
            app.MapGroup("RoomType")
            .MapGet(GetTodoItemsWithPagination)
            .MapPost(CreateRoomType)
            .MapPut(UpdateRoomType, $"{{roomTypeID}}")
            .MapDelete(DeleteRoomType, $"{{roomTypeID}}");
        }

        [AllowAnonymous]
        private async Task<IResult> DeleteRoomType([FromServices] ISender sender, [FromQuery] string roomTypeID)
        {
            var command = new DeleteRoomTypeCommand { RoomTypeID = roomTypeID };
            await sender.Send(command);
            return Results.Ok(new
            {
                Message = "Delete room type successfully"
            });

        }

        [AllowAnonymous]
        private async Task<IResult> CreateRoomType([FromServices] ISender sender, [FromBody] CreateRoomTypeCommand command)
        {

            var inserted = await sender.Send(command);
            return Results.Ok(new
            {
                Message = "Create Room Type Successfully",
                StatusCode = 200,
                Data = inserted
            });

        }
        [AllowAnonymous]
        private async Task<IResult> UpdateRoomType(
            [FromServices] ISender sender,
            [FromQuery] string roomTypeID,
            [FromBody] UpdateRoomTypeCommand command)
        {
            var updated = await sender.Send(new UpdateRoomTypeWithIdCommand
            {
                RoomTypeID = roomTypeID,
                Command = command
            });

            return Results.Ok(new
            {
                Message = "Update Room Successfully",
                Data = updated
            });
        }
        [AllowAnonymous]
        public Task<PaginatedList<RoomTypeDTO>> GetTodoItemsWithPagination([FromServices] ISender sender, [AsParameters] GetRoomTypesQuery query)
        {
            return sender.Send(query);
        }

    }
}