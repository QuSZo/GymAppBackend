using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.ValueObjects.Email;
using GymAppBackend.Core.ValueObjects.Password;
using GymAppBackend.Core.ValueObjects.Role;

namespace GymAppBackend.Application.Users.Commands;

internal sealed class SignUpHandler : ICommandHandler<SignUpCommand, CreateOrUpdateResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(SignUpCommand command)
    {
        var email = new Email(command.Email);
        var password = new Password(command.Password);
        var role = new Role(command.Role);

        if (await _userRepository.GetByEmailAsync(email) != null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        var securedPassword = _passwordManager.Secure(password);
        var user = User.Create(command.UserId, email, securedPassword, role, _clock.Current());
        await _userRepository.AddAsync(user);

        return new CreateOrUpdateResponse(command.UserId);
    }
}