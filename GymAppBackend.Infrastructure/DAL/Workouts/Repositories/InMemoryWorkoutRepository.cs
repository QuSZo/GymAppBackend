using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Repositories;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Repositories;

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

    public async Task<IEnumerable<Workout>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _workouts;
    }

    public async Task<Workout> GetAsync(Guid id)
    {
        await Task.CompletedTask;
        return _workouts.SingleOrDefault(w => w.Id == id);
    }

    public async Task AddAsync(Workout workout)
    {
        await Task.CompletedTask;
        _workouts.Add(workout);
    }

    public async Task UpdateAsync(Workout workout)
    {
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.CompletedTask;
        var workout = _workouts.Single(w => w.Id == id);
        _workouts.Remove(workout);
    }
}