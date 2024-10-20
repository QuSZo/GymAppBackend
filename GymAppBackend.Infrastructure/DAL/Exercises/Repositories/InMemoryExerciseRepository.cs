using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.Exercises.Repositories;

namespace GymAppBackend.Infrastructure.DAL.Exercises.Repositories;

public class InMemoryExerciseRepository : IExerciseRepository
{
    private readonly List<Exercise> _exercises;

    public InMemoryExerciseRepository()
    {
        _exercises = new List<Exercise>();
    }

    public async Task<IEnumerable<Exercise>> GetAllByWorkoutIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return _exercises.Where(exercise => exercise.Workout.Id == id);
    }
}