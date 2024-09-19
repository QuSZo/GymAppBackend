using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Repositories;

namespace GymAppBackend.Infrastructure.DAL.Repositories;

internal sealed class InMemoryWorkoutRepository : IWorkoutRepository
{
    private readonly List<Workout> _workouts;

    public InMemoryWorkoutRepository()
    {
        _workouts = new List<Workout>();
    }

    public async Task<IEnumerable<Workout>> GetWorkoutsAsync()
    {
        await Task.CompletedTask;
        return _workouts;
    }
}