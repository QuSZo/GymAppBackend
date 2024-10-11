using GymAppBackend.Core.Workouts.Entities;

namespace GymAppBackend.Core.Workouts.Repositories;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetAllAsync();
    Task<Workout> GetAsync(Guid id);
    Task AddAsync(Workout workout);
    Task UpdateAsync(Workout workout);
    Task DeleteAsync(Guid id);
}