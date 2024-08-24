using Microsoft.AspNetCore.Authorization;
using MyWebApi.Application.RoomTypes.Commands.CreateRoomType;

namespace MyWebApi.Web.Endpoints
{
    public class RoomTypeEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup("RoomType").MapPost(CreateRoomType);
        }

        [AllowAnonymous]
        private Task<string> CreateRoomType(ISender sender, CreateRoomTypeCommand command)
        {
            return sender.Send(command);
        }
    }
}