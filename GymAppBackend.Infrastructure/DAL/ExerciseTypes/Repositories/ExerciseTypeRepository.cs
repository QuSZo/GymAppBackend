using GymAppBackend.Core.ExerciseTypes.Entities;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;

internal sealed class ExerciseTypeRepository : IExerciseTypeRepository
{
    private readonly GymAppDbContext _dbContext;

    public ExerciseTypeRepository(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseType?> GetAsync(Guid id)
    {
        return await _dbContext.ExerciseTypes
            .Include(exerciseType => exerciseType.ExerciseCategory)
            .SingleOrDefaultAsync(exerciseType => exerciseType.Id == id);
    }
}