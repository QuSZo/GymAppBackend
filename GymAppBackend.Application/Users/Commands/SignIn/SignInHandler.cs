using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Security.DTO;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;

namespace GymAppBackend.Application.Users.Commands.SignIn;

internal sealed class SignInHandler : ICommandHandler<SignInCommand, JwtDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
    }

    public async Task<JwtDto> HandleAsync(SignInCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role);
        return jwt;
    }
}