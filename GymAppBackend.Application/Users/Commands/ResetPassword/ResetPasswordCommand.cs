using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Users.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Email) : ICommand;