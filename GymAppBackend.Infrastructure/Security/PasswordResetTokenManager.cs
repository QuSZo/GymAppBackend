using GymAppBackend.Application.Security;
using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Infrastructure.Security;

public class PasswordResetTokenManager : IPasswordResetTokenManager
{
    public PasswordResetTokenManager()
    {
    }

    public async Task<string> GeneratePasswordResetToken(User user)
    {
        throw new NotImplementedException();
    }
}