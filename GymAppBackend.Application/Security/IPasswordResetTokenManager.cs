using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Application.Security;

public interface IPasswordResetTokenManager
{
    Task<string> GeneratePasswordResetToken(User user);
}