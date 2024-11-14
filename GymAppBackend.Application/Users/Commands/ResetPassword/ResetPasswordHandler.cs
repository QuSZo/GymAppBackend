using System.Security.Authentication;
using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.PasswordResetTokens.Repositories;
using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.ValueObjects.Password;

namespace GymAppBackend.Application.Users.Commands.ResetPassword;

internal sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordResetTokenManager _passwordResetTokenManager;
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public ResetPasswordHandler(
        IUserRepository userRepository,
        IPasswordResetTokenManager passwordResetTokenManager,
        IPasswordResetTokenRepository passwordResetTokenRepository,
        IClock clock, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _passwordResetTokenManager = passwordResetTokenManager;
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _clock = clock;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(ResetPasswordCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user == null)
        {
            throw new InvalidCredentialException();
        }

        var hashedToken = _passwordResetTokenManager.HashToken(command.Token);
        var passwordResetToken = await _passwordResetTokenRepository.GetByTokenAsync(hashedToken);

        if (passwordResetToken == null ||
            passwordResetToken.User.Id != user.Id ||
            passwordResetToken.Expires < _clock.Current())
        {
            throw new InvalidCredentialException();
        }

        var password = new Password(command.Password);
        var securedPassword = _passwordManager.Secure(password);
        user.ResetPassword(securedPassword);
        await _userRepository.UpdateAsync(user);
    }
}