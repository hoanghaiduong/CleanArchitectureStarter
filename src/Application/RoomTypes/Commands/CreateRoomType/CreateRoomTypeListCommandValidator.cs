
using MyWebApi.Application.Common.Interfaces;

namespace MyWebApi.Application.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeListCommandValidator : AbstractValidator<CreateRoomTypeCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomTypeListCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
        }
        public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}