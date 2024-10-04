using GymAppBackend.Core.Entities;

namespace GymAppBackend.Core.Repositories;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetAllAsync();
    Task<Workout> GetAsync(Guid id);
    Task AddAsync(Workout workout);
    Task UpdateAsync(Workout workout);
    Task DeleteAsync(Guid id);
}