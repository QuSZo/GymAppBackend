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

    public async Task<Exercise?> GetAsync(Guid id)
    {
        await Task.CompletedTask;
        return _exercises.SingleOrDefault(exercise => exercise.Id == id);
    }

    public async Task AddAsync(Exercise exercise)
    {
        await Task.CompletedTask;
        _exercises.Add(exercise);
    }

    public Task UpdateRangeAsync(IEnumerable<Exercise> exercises)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Exercise exercise)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}