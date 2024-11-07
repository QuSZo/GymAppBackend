using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Users.Commands;

public sealed record SignUpCommand(string Email, string Password, string Role) : ICommand<CreateOrUpdateResponse>
{
    internal Guid UserId { get; init; } = Guid.NewGuid();
}