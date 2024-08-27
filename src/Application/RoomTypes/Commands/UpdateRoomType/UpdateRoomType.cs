

using Ardalis.GuardClauses;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Application.RoomTypes.Commands.UpdateRoomType
{
    public record UpdateRoomTypeWithIdCommand : IRequest<RoomType>
    {
        public string RoomTypeID { get; set; } = string.Empty;
        public UpdateRoomTypeCommand Command { get; set; } = new UpdateRoomTypeCommand();
    }
    public record UpdateRoomTypeCommand : IRequest<RoomType>
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal PricePerNight { get; init; }

        public int Capacity { get; init; }
    }
    public class UpdateRoomTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateRoomTypeWithIdCommand, RoomType>
    {
        public async Task<RoomType> Handle(UpdateRoomTypeWithIdCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.RoomTypes.FindAsync(new object[] { request.RoomTypeID }, cancellationToken);
            Guard.Against.NotFound(request.RoomTypeID, entity);

            // Use the original command's properties to update the entity
            entity.Name = request.Command.Name;
            entity.Description = request.Command.Description;
            entity.PricePerNight = request.Command.PricePerNight;
            entity.Capacity = request.Command.Capacity;

            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }


    }
}
