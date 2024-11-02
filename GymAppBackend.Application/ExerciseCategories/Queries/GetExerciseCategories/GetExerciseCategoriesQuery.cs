using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.ExerciseCategories.Queries.DTO;

namespace GymAppBackend.Application.ExerciseCategories.Queries.GetExerciseCategories;

public class GetExerciseCategoriesQuery : IQuery<IEnumerable<ExerciseCategoryDto>>
{
}