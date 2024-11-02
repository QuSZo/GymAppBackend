using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseTypes.Queries.Dto;

namespace GymAppBackend.Application.ExerciseTypes.Queries.GetExerciseTypes;

public class GetExerciseTypesQuery : IQuery<IEnumerable<ExerciseTypeDto>>
{
}