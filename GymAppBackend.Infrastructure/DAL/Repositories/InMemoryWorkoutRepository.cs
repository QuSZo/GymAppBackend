using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Entities;
using GymAppBackend.Core.Repositories;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Infrastructure.DAL.Repositories;

internal sealed class InMemoryWorkoutRepository : IWorkoutRepository
{
    private readonly List<Workout> _workouts;

    public InMemoryWorkoutRepository(IClock clock)
    {
        _workouts = new List<Workout>
        {
            Workout.Create(Guid.NewGuid(), new Date(clock.Current())),
            Workout.Create(Guid.NewGuid(), new Date(clock.Current()))
        };
    }

    public async Task<IEnumerable<Workout>> GetWorkoutsAsync()
    {
        await Task.CompletedTask;
        return _workouts;
    }
}