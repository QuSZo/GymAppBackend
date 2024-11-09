using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security.DTO;

namespace GymAppBackend.Application.Users.Commands.SignIn;

public sealed record SignInCommand(string Email, string Password) : ICommand<JwtDto>;