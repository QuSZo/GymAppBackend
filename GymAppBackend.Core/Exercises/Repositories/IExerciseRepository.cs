using GymAppBackend.Core.Exercises.Entities;

namespace GymAppBackend.Core.Exercises.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetAllByWorkoutIdAsync(Guid id);
    Task<Exercise?> GetAsync(Guid id);
    Task AddAsync(Exercise exercise);
    Task UpdateRangeAsync(IEnumerable<Exercise> exercises);
    Task DeleteAsync(Exercise exercise);
}