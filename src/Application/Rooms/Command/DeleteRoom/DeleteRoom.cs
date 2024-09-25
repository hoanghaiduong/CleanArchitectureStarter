

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Application.Rooms.Command.DeleteRoom
{
    public class DeleteRoomCommand(string roomID) : IRequest<bool>
    {
        [JsonIgnore]
        public string RoomID { get; set; } = roomID;
    }
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FindAsync([request.RoomID], cancellationToken);
            if (room == null) return false;
            _context.Rooms.Remove(room!);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}