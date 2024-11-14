using System.Text;
using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Emails;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.ValueObjects.Password;

namespace GymAppBackend.Application.Users.Commands.ResetPassword;

internal sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailClient _emailClient;
    private readonly IPasswordManager _passwordManager;
    private static readonly Random _random = new Random();

    public ResetPasswordHandler(IUserRepository userRepository, IEmailClient emailClient, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _emailClient = emailClient;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(ResetPasswordCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        var password = new Password(GenerateRandomPassword());
        var securedPassword = _passwordManager.Secure(password);

        user.ResetPassword(securedPassword);
        await _userRepository.UpdateAsync(user);

        await _emailClient.SendEmailAsync(user.Email, "Reset Password", $"This is your new password: {password}");
    }

    private string GenerateRandomPassword()
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
    }
}