using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.ValueObjects.Email;

namespace GymAppBackend.Core.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(Email email);
    Task AddAsync(User user);
}