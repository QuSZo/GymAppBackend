using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Users.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : ICommand;