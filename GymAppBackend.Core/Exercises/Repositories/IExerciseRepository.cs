using GymAppBackend.Core.Exercises.Entities;

namespace GymAppBackend.Core.Exercises.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetAllByWorkoutIdAsync(Guid id);
}