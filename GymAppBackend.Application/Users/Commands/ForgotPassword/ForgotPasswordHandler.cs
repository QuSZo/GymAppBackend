using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Emails;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;

namespace GymAppBackend.Application.Users.Commands.ForgotPassword;

internal sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailClient _emailClient;
    private readonly IPasswordResetTokenManager _passwordResetTokenManager;
    //private static readonly Random _random = new Random();

    public ForgotPasswordHandler(IUserRepository userRepository, IEmailClient emailClient, IPasswordResetTokenManager passwordResetTokenManager)
    {
        _userRepository = userRepository;
        _emailClient = emailClient;
        _passwordResetTokenManager = passwordResetTokenManager;
    }

    public async Task HandleAsync(ForgotPasswordCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        var token = await _passwordResetTokenManager.GeneratePasswordResetTokenAsync(user);

        /*var password = new Password(GenerateRandomPassword());
        var securedPassword = _passwordManager.Secure(password);

        user.ResetPassword(securedPassword);
        await _userRepository.UpdateAsync(user);*/

        var resetPasswordUrl = $"http://localhost:3000/reset-password?token={token}&email={user.Email}";
        await _emailClient.SendEmailAsync(user.Email, "Reset Password", $"Click this link to continue resetting your password: {resetPasswordUrl}");
    }

    /*private string GenerateRandomPassword()
    {
        const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string allChars = letters + digits;

        var password = new StringBuilder();

        password.Append(letters[_random.Next(letters.Length)]);
        password.Append(digits[_random.Next(digits.Length)]);

        for (int i = 2; i < 8; i++)
        {
            password.Append(allChars[_random.Next(allChars.Length)]);
        }

        return new string(password.ToString().OrderBy(_ => _random.Next()).ToArray());
    }*/
}