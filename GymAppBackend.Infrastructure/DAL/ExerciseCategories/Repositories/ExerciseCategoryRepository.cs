using GymAppBackend.Core.ExerciseCategories.Entities;
using GymAppBackend.Core.ExerciseCategories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;

internal sealed class ExerciseCategoryRepository : IExerciseCategoryRepository
{
    private readonly GymAppDbContext _dbContext;

    public ExerciseCategoryRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseCategory> GetAsync(Guid id)
    {
        return await _dbContext.ExerciseCategories.SingleOrDefaultAsync(exerciseCategory => exerciseCategory.Id == id);
    }
}