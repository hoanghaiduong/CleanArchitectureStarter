
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Application.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeListCommandValidator : AbstractValidator<UpdateRoomTypeWithIdCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRoomTypeListCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Command.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
        }
        public async Task<bool> BeUniqueTitle(UpdateRoomTypeWithIdCommand model,string name, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes
                .Where(r => r.RoomTypeID != model.RoomTypeID)
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}