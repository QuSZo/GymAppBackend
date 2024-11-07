using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.ValueObjects.Email;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Users.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly GymAppDbContext _dbContext;

    public UserRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmailAsync(Email email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}