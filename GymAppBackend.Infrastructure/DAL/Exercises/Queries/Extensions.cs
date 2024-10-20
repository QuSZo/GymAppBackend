using GymAppBackend.Application.Exercises.Queries.DTO;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Infrastructure.DAL.ExerciseSets.Queries;

namespace GymAppBackend.Infrastructure.DAL.Exercises.Queries;

public static class Extensions
{
    public static ExerciseDto AsDto(this Exercise exercise)
    {
        return new()
        {
            ExerciseNumber = exercise.ExerciseNumber,
            ExerciseTypeName = exercise.ExerciseType.Name,
            ExerciseSets = exercise.ExerciseSets.Select(exerciseSet => exerciseSet.AsDto()),
        };
    }
}