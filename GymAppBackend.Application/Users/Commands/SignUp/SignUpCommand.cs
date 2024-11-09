using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Users.Commands.SignUp;

public sealed record SignUpCommand(string Email, string Passwword) : ICommand<CreateOrUpdateResponse>
{
    internal Guid UserId { get; init; } = Guid.NewGuid();
}