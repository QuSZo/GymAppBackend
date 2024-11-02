using GymAppBackend.Application.ExerciseTypes.Queries.Dto;
using GymAppBackend.Core.ExerciseTypes.Entities;

namespace GymAppBackend.Infrastructure.DAL.ExerciseTypes.Queries;

public static class Extensions
{
    public static ExerciseTypeDto AsDto(this ExerciseType exerciseType)
    {
        return new()
        {
            Id = exerciseType.Id,
            Name = exerciseType.Name,
            ExerciseCategoryId = exerciseType.ExerciseCategoryId,
        };
    }
}