using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Repositories;

internal sealed class WorkoutRepository : IWorkoutRepository
{
    private readonly GymAppDbContext _dbContext;

    public WorkoutRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Workout>> GetAllAsync()
    {
        return await _dbContext.Workouts
            .Include(workout => workout.Exercises)
            .ToListAsync();
    }

    public async Task<Workout?> GetAsync(Guid id)
    {
        return await _dbContext.Workouts
            .Include(workout => workout.Exercises)
            .SingleOrDefaultAsync(workout => workout.Id == id);
    }

    public async Task<Workout?> GetByDateAsync(Date date)
    {
        return await _dbContext.Workouts
            .Include(workout => workout.Exercises)
            .SingleOrDefaultAsync(workout => workout.Date == date);
    }

    public async Task AddAsync(Workout workout)
    {
        await _dbContext.Workouts.AddAsync(workout);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Workout workout)
    {
        _dbContext.Workouts.Update(workout);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Workout workout)
    {
        _dbContext.Workouts.Remove(workout);
        await _dbContext.SaveChangesAsync();
    }
}