using System.Security.Cryptography;
using System.Text;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.PasswordResetTokens.Entities;
using GymAppBackend.Core.PasswordResetTokens.Repositories;
using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Infrastructure.Security;

public class PasswordResetTokenManager : IPasswordResetTokenManager
{
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IClock _clock;

    public PasswordResetTokenManager(IPasswordResetTokenRepository passwordResetTokenRepository, IClock clock)
    {
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _clock = clock;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        string bytesBase64Url;
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[64];
            rng.GetBytes(bytes);
            bytesBase64Url = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
        }

        string hashedPasswordResetToken;
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(bytesBase64Url));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            hashedPasswordResetToken = builder.ToString();
        }

        var passwordResetToken = PasswordResetToken.Create(hashedPasswordResetToken, _clock.Current().AddMinutes(20), user);
        await _passwordResetTokenRepository.AddAsync(passwordResetToken);

        return bytesBase64Url;
    }

    public string HashToken(string passwordResetToken)
    {
        string hashedPasswordResetToken;
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwordResetToken));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            hashedPasswordResetToken = builder.ToString();
        }

        return hashedPasswordResetToken;
    }
}