using GymAppBackend.Core.PasswordResetTokens.Entities;
using GymAppBackend.Core.ValueObjects.Email;

namespace GymAppBackend.Core.PasswordResetTokens.Repositories;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByTokenAsync(string token);
    Task AddAsync(PasswordResetToken passwordResetToken);
}