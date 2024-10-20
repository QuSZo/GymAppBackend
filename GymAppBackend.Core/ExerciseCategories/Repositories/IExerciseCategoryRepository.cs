using GymAppBackend.Core.ExerciseCategories.Entities;

namespace GymAppBackend.Core.ExerciseCategories.Repositories;

public interface IExerciseCategoryRepository
{
    Task<ExerciseCategory> GetAsync(Guid id);
}