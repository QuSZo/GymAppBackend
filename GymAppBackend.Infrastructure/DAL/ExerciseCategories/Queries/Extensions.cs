using GymAppBackend.Application.ExerciseCategories.Queries.DTO;
using GymAppBackend.Core.ExerciseCategories.Entities;

namespace GymAppBackend.Infrastructure.DAL.ExerciseCategories.Queries;

public static class Extensions
{
    public static ExerciseCategoryDto AsDto(this ExerciseCategory exerciseCategory)
    {
        return new()
        {
            Id = exerciseCategory.Id,
            Name = exerciseCategory.Name,
        };
    }
}