using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Application.Security;

public interface IPasswordResetTokenManager
{
    Task<string> GeneratePasswordResetTokenAsync(User user);
    string HashToken(string passwordResetToken);
}