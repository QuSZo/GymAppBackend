using GymAppBackend.Application.ExerciseSets.Queries.DTO;
using GymAppBackend.Core.ExerciseSets.Entities;

namespace GymAppBackend.Infrastructure.DAL.ExerciseSets.Queries;

public static class Extensions
{
    public static ExerciseSetDto AsDto(this ExerciseSet exerciseSet)
    {
        return new()
        {
            Id = exerciseSet.Id,
            Quantity = exerciseSet.Quantity,
            Reps = exerciseSet.Reps,
            SetNumber = exerciseSet.SetNumber,
        };
    }
}