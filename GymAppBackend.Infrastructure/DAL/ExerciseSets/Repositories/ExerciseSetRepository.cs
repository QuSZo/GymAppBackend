using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseSets.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.ExerciseSets.Repositories;

internal sealed class ExerciseSetRepository : IExerciseSetRepository
{
    private readonly GymAppDbContext _dbContext;

    public ExerciseSetRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseSet?> GetAsync(Guid id)
    {
        return await _dbContext.ExerciseSets.SingleOrDefaultAsync(exerciseSet => exerciseSet.Id == id);
    }

    public async Task AddAsync(ExerciseSet exerciseSet)
    {
        await _dbContext.ExerciseSets.AddAsync(exerciseSet);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExerciseSet exerciseSet)
    {
        _dbContext.ExerciseSets.Update(exerciseSet);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ExerciseSet exerciseSet)
    {
        _dbContext.ExerciseSets.Remove(exerciseSet);
        await _dbContext.SaveChangesAsync();
    }
}