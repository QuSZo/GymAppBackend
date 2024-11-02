using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseCategories.Queries.DTO;
using GymAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.ExerciseCategories.Queries.GetExerciseCategories;

internal sealed class GetExerciseCategoriesHandler : IQueryHandler<GetExerciseCategoriesQuery, IEnumerable<ExerciseCategoryDto>>
{
    private readonly GymAppDbContext _dbContext;

    public GetExerciseCategoriesHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ExerciseCategoryDto>> HandleAsync(GetExerciseCategoriesQuery query)
    {
        var exerciseCategories = await _dbContext.ExerciseCategories
            .AsNoTracking()
            .Select(exerciseCategory => exerciseCategory.AsDto())
            .ToListAsync();

        return exerciseCategories;
    }
}