using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseTypes.Queries.Dto;
using GymAppBackend.Application.ExerciseTypes.Queries.GetExerciseTypes;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.ExerciseTypes.Queries.GetExerciseTypes;

internal sealed class GetExerciseTypesHandler : IQueryHandler<GetExerciseTypesQuery, IEnumerable<ExerciseTypeDto>>
{
    private readonly GymAppDbContext _dbContext;

    public GetExerciseTypesHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ExerciseTypeDto>> HandleAsync(GetExerciseTypesQuery query)
    {
        var exerciseTypes = await _dbContext.ExerciseTypes
            .AsNoTracking()
            .Select(exerciseType => exerciseType.AsDto())
            .ToListAsync();

        return exerciseTypes;
    }
}