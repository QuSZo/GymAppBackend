using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.Exercises.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Exercises.Repositories;

internal sealed class ExerciseRepository : IExerciseRepository
{
    private readonly GymAppDbContext _dbContext;

    public ExerciseRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Exercise>> GetAllByWorkoutIdAsync(Guid id)
    {
        return await _dbContext.Exercises
            .Include(exercise => exercise.ExerciseType)
            .Include(exercise => exercise.Workout)
                .ThenInclude(workout => workout.User)
            .Include(exercise => exercise.ExerciseSets)
            .Where(exercise => exercise.Workout.Id == id)
            .OrderBy(exercise => exercise.ExerciseNumber)
            .ToListAsync();
    }

    public async Task<Exercise?> GetAsync(Guid id)
    {
        return await _dbContext.Exercises
            .Include(exercise => exercise.ExerciseType)
            .Include(exercise => exercise.Workout)
                .ThenInclude(workout => workout.User)
            .Include(exercise => exercise.ExerciseSets)
            .SingleOrDefaultAsync(exercise => exercise.Id == id);
    }

    public async Task AddAsync(Exercise exercise)
    {
        await _dbContext.Exercises.AddAsync(exercise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Exercise exercise)
    {
        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync();
    }
}