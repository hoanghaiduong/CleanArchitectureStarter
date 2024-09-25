


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Rooms.Command.CreateRoom;
using MyWebApi.Application.Rooms.Command.DeleteRoom;
using MyWebApi.Application.Rooms.Command.UpdateRoom;
using MyWebApi.Application.Rooms.Queries;


namespace MyWebApi.Web.Endpoints
{
    [Produces("application/json")]
    public class RoomEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup("Room")
            .MapGet(GetAllRoom, "all")
            .MapGet(GetRoomByID)
            .MapPost(CreateRoom)
            .MapPut(UpdateRoomByID)
            .MapDelete(DeleteRoomByID);
        }
        [AllowAnonymous]
        private async Task<IResult> CreateRoom([FromServices] ISender sender,
        [FromBody] CreateRoomCommand command)
        {
            try
            {

                var room = await sender.Send(command);
                return room == null
                    ? Results.BadRequest()
                    : Results.Ok(new
                    {
                        Message = "Create room successfully",
                        Data = room
                    }
                );

            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ex.Message
                });

            }
        }
        [AllowAnonymous]
        private async Task<IResult> GetAllRoom([FromServices] ISender sender, [AsParameters] GetRoomsQuery command)
        {
            try
            {
                var data = await sender.Send(command);
                return Results.Ok(new
                {
                    Message = "Get all rooms Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { StatusCode = 400, ex.Message });
            }
        }
        [AllowAnonymous]
        private async Task<IResult> GetRoomByID([FromServices] ISender sender, [FromQuery] string roomID)
        {
            var data = await sender.Send(new GetRoomQuery(roomID));
            return data == null
                ? Results.NotFound(new { Message = "NOT_FOUND" })
                : Results.Ok(new
                {
                    Message = "Get all rooms Successfully",
                    Data = data
                });
        }

        [AllowAnonymous]
        private async Task<IResult> UpdateRoomByID([FromServices] ISender sender, [FromQuery] string roomID, [FromBody] UpdateRoomCommand command)
        {
            try
            {
                var data = await sender.Send(new UpdateRoomCommandWithID { Command = command, RoomID = roomID });
                return data == null
                    ? Results.NotFound(new { Message = "NOT_FOUND" })
                    : Results.Ok(new
                    {
                        Message = "Update Room Successfully",
                        Data = data
                    });
            }

            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        private async Task<IResult> DeleteRoomByID([FromServices] ISender sender, [FromQuery] string roomID)
        {
            try
            {

                var data = await sender.Send(new DeleteRoomCommand(roomID));
                return !data
                    ? Results.NotFound(new { Message = "NOT_FOUND" })
                    : Results.Ok(new
                    {
                        Message = "Delete Room Successfully",
                        Data = data
                    });
            }

            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}