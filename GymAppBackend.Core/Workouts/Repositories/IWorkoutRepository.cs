using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;
using GymAppBackend.Core.Workouts.Entities;

namespace GymAppBackend.Core.Workouts.Repositories;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetAllAsync();
    Task<Workout?> GetAsync(Guid id);
    Task<Workout?> GetByDateForUserAsync(Date date, Guid userId);
    Task AddAsync(Workout workout);
    Task UpdateAsync(Workout workout);
    Task DeleteAsync(Workout workout);
}