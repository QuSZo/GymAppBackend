using GymAppBackend.Core.Exercises.Entities;

namespace GymAppBackend.Core.Exercises.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetAllByWorkoutIdAsync(Guid id);
    Task<Exercise?> GetAsync(Guid id);
    Task AddAsync(Exercise exercise);
    Task DeleteAsync(Exercise exercise);
}