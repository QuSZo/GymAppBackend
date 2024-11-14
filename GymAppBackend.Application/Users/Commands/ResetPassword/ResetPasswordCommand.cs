using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Users.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Token, string Email, string Password) : ICommand;