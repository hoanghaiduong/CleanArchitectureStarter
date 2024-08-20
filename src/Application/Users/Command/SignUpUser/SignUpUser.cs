

// using MyWebApi.Application.Common.Interfaces;

// namespace MyWebApi.Application.Users.Command.SignUpUser
// {
//     public record SignUpUserCommand : IRequest<int>
//     {
//          public string? FirstName { get; init; }
//         public string? LastName { get; init; }
//         public required string Email { get; init; }
//         public required string Password { get; init; }
//     }

//     public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, int>
//     {
//         private readonly UserManager<ApplicationUser> _userManager;

//         public SignUpUserCommandHandler(IApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<int> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
//         {
           
//         }
//     }
// }