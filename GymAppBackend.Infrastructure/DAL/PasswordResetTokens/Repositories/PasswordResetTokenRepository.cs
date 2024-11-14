using GymAppBackend.Core.PasswordResetTokens.Entities;
using GymAppBackend.Core.PasswordResetTokens.Repositories;
using GymAppBackend.Core.ValueObjects.Email;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.PasswordResetTokens.Repositories;

internal sealed class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly GymAppDbContext _dbContext;

    public PasswordResetTokenRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
    {
        return await _dbContext.PasswordResetTokens
            .Include(passwordResetToken => passwordResetToken.User)
            .SingleOrDefaultAsync(passwordResetToken => passwordResetToken.Token == token);
    }

    public async Task AddAsync(PasswordResetToken passwordResetToken)
    {
        await _dbContext.PasswordResetTokens.AddAsync(passwordResetToken);
        await _dbContext.SaveChangesAsync();
    }
}